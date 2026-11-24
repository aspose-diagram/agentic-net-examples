using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <jsonPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
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

        // Load JSON mapping of shape names to user‑defined cell values.
        List<ShapeCellData> mappings;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            mappings = JsonSerializer.Deserialize<List<ShapeCellData>>(jsonContent);
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

        // Load the Visio diagram and apply the user‑defined cell values.
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Build a quick lookup: shape name (NameU) -> Shape instance.
                var shapeLookup = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (!string.IsNullOrEmpty(shape.NameU))
                        {
                            // If multiple shapes share the same name, the first one is kept.
                            if (!shapeLookup.ContainsKey(shape.NameU))
                                shapeLookup[shape.NameU] = shape;
                        }
                    }
                }

                // Iterate over each mapping entry from the JSON.
                foreach (var entry in mappings)
                {
                    if (string.IsNullOrWhiteSpace(entry.ShapeName) || entry.Cells == null)
                        continue; // Skip invalid entries.

                    if (!shapeLookup.TryGetValue(entry.ShapeName, out Shape targetShape))
                    {
                        Console.Error.WriteLine($"Shape not found in diagram: {entry.ShapeName}");
                        continue;
                    }

                    // Apply each user‑defined cell value to the shape.
                    foreach (var cellKvp in entry.Cells)
                    {
                        string cellName = cellKvp.Key;
                        string cellValue = cellKvp.Value ?? string.Empty;

                        // Try to find an existing user‑defined cell.
                        User existingUser = null;
                        foreach (User user in targetShape.Users)
                        {
                            if (string.Equals(user.NameU, cellName, StringComparison.OrdinalIgnoreCase))
                            {
                                existingUser = user;
                                break;
                            }
                        }

                        if (existingUser != null)
                        {
                            // Update the value of the existing cell.
                            existingUser.Value.Val = cellValue;
                        }
                        else
                        {
                            // Create a new user‑defined cell and add it to the shape.
                            User newUser = new User();
                            newUser.NameU = cellName;
                            newUser.Value.Val = cellValue;
                            targetShape.Users.Add(newUser);
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper class representing the JSON structure.
    private class ShapeCellData
    {
        public string ShapeName { get; set; }
        public Dictionary<string, string> Cells { get; set; }
    }
}