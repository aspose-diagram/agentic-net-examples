using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramJsonImporter
{
    // Represents a single shape entry in the JSON file.
    public class ShapeData
    {
        // Universal name of the shape to match (case‑insensitive).
        public string NameU { get; set; }

        // Dictionary of user‑defined cell names and their string values.
        public Dictionary<string, string> Cells { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // 0 - path to the source Visio diagram (e.g., .vsdx)
            // 1 - path to the JSON file containing cell values
            // 2 - path where the updated diagram will be saved
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramJsonImporter <diagramPath> <jsonPath> <outputPath>");
                return;
            }

            string diagramPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the JSON content.
                string jsonContent = File.ReadAllText(jsonPath);
                List<ShapeData> shapeDataList = JsonSerializer.Deserialize<List<ShapeData>>(jsonContent);

                if (shapeDataList == null)
                {
                    Console.WriteLine("JSON file does not contain any shape data.");
                    return;
                }

                // Iterate over each shape entry from the JSON.
                foreach (ShapeData shapeData in shapeDataList)
                {
                    if (string.IsNullOrWhiteSpace(shapeData.NameU) || shapeData.Cells == null)
                        continue; // Skip invalid entries.

                    // Search for a matching shape by its universal name across all pages.
                    Shape matchingShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (string.Equals(shape.NameU, shapeData.NameU, StringComparison.OrdinalIgnoreCase))
                            {
                                matchingShape = shape;
                                break;
                            }
                        }
                        if (matchingShape != null)
                            break;
                    }

                    if (matchingShape == null)
                    {
                        Console.WriteLine($"Shape with NameU '{shapeData.NameU}' not found in the diagram.");
                        continue;
                    }

                    // Assign or update each user‑defined cell.
                    foreach (KeyValuePair<string, string> cellEntry in shapeData.Cells)
                    {
                        string cellName = cellEntry.Key;
                        string cellValue = cellEntry.Value ?? string.Empty;

                        // Look for an existing User cell with the same name.
                        User existingUser = null;
                        foreach (User user in matchingShape.Users)
                        {
                            if (string.Equals(user.Name, cellName, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(user.NameU, cellName, StringComparison.OrdinalIgnoreCase))
                            {
                                existingUser = user;
                                break;
                            }
                        }

                        if (existingUser != null)
                        {
                            // Update the value of the existing user‑defined cell.
                            existingUser.Value.Val = cellValue;
                        }
                        else
                        {
                            // Create a new user‑defined cell and add it to the shape.
                            User newUser = new User
                            {
                                Name = cellName,
                                NameU = cellName,
                                Value = { Val = cellValue }
                            };
                            matchingShape.Users.Add(newUser);
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}