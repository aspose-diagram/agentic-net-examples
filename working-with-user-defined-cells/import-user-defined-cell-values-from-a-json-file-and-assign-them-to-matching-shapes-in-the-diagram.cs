using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed or pass via command‑line arguments
                string diagramPath = "input.vsdx";
                string jsonPath = "values.json";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize the JSON file.
                // Expected format:
                // {
                //   "ShapeNameU1": { "CellName1": "Value1", "CellName2": "Value2" },
                //   "ShapeNameU2": { "CellNameA": "ValueA" }
                // }
                string jsonContent = File.ReadAllText(jsonPath);
                var shapeCellMap = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);

                if (shapeCellMap == null)
                {
                    Console.WriteLine("JSON deserialization returned null. Exiting.");
                    return;
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Match shape by its universal name (NameU)
                        if (shapeCellMap.TryGetValue(shape.NameU, out var cellValues))
                        {
                            foreach (var kvp in cellValues)
                            {
                                string cellName = kvp.Key;
                                string cellValue = kvp.Value;

                                // Look for an existing user‑defined cell with the same name
                                User existingUser = null;
                                foreach (User user in shape.Users)
                                {
                                    if (user.Name == cellName)
                                    {
                                        existingUser = user;
                                        break;
                                    }
                                }

                                if (existingUser != null)
                                {
                                    // Update existing cell value
                                    existingUser.Value.Val = cellValue;
                                }
                                else
                                {
                                    // Create a new user‑defined cell and add it to the shape
                                    User newUser = new User
                                    {
                                        Name = cellName,
                                        Value = { Val = cellValue }
                                    };
                                    shape.Users.Add(newUser);
                                }
                            }
                        }
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