using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing a shape and its user-defined cell values from JSON
    private class ShapeCellMapping
    {
        public string ShapeName { get; set; }
        public Dictionary<string, string> Cells { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: diagram path, JSON path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <jsonPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load JSON mappings
        List<ShapeCellMapping> mappings;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            mappings = JsonSerializer.Deserialize<List<ShapeCellMapping>>(jsonContent);
            if (mappings == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON content.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON file: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply user-defined cell values
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Find a mapping that matches the shape's name
                        foreach (ShapeCellMapping map in mappings)
                        {
                            if (shape.Name == map.ShapeName)
                            {
                                // Apply each user-defined cell value
                                foreach (KeyValuePair<string, string> kvp in map.Cells)
                                {
                                    // Search for an existing user cell with the same name
                                    User targetUser = null;
                                    foreach (User user in shape.Users)
                                    {
                                        if (user.NameU == kvp.Key)
                                        {
                                            targetUser = user;
                                            break;
                                        }
                                    }

                                    // If not found, create a new user-defined cell
                                    if (targetUser == null)
                                    {
                                        targetUser = new User();
                                        targetUser.NameU = kvp.Key;
                                        shape.Users.Add(targetUser);
                                    }

                                    // Assign the value to the cell
                                    targetUser.Value.Val = kvp.Value;
                                }

                                // Once matched and processed, break to avoid duplicate work
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}