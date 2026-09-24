using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramUserCellUpdater
{
    // Model representing the JSON structure for shape updates
    public class ShapeUpdate
    {
        public string ShapeNameU { get; set; }
        public Dictionary<string, string> UserCells { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string jsonPath = "cellValues.json";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the JSON file
                string jsonContent = File.ReadAllText(jsonPath);
                List<ShapeUpdate> updates = JsonSerializer.Deserialize<List<ShapeUpdate>>(jsonContent);

                if (updates == null)
                {
                    Console.WriteLine("No updates found in JSON.");
                    return;
                }

                // Iterate through each update entry
                foreach (var update in updates)
                {
                    bool shapeFound = false;

                    // Search all pages for a shape with matching NameU
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (string.Equals(shape.NameU, update.ShapeNameU, StringComparison.OrdinalIgnoreCase))
                            {
                                shapeFound = true;

                                // Apply each user-defined cell value
                                foreach (var kvp in update.UserCells)
                                {
                                    string cellName = kvp.Key;
                                    string cellValue = kvp.Value;

                                    // Try to find an existing User cell
                                    User existingUser = null;
                                    foreach (User user in shape.Users)
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
                                        // Update existing cell
                                        existingUser.Value.Val = cellValue;
                                    }
                                    else
                                    {
                                        // Create a new User cell
                                        User newUser = new User();
                                        newUser.Name = cellName;
                                        newUser.NameU = cellName;
                                        newUser.Value.Val = cellValue;
                                        shape.Users.Add(newUser);
                                    }
                                }

                                // Once the shape is processed, break out of inner loops
                                break;
                            }
                        }

                        if (shapeFound) break;
                    }

                    if (!shapeFound)
                    {
                        Console.WriteLine($"Shape with NameU '{update.ShapeNameU}' not found.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}