using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold information about shapes that are not part of any group
                List<SimpleShapeInfo> ungroupedShapes = new List<SimpleShapeInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip group shapes themselves
                        if (shape.Type == TypeValue.Group)
                            continue;

                        // If the shape is not inside any group, record it
                        if (!shape.IsInGroup())
                        {
                            ungroupedShapes.Add(new SimpleShapeInfo
                            {
                                Id = shape.ID,
                                Name = shape.Name ?? string.Empty,
                                Page = page.Name ?? string.Empty
                            });
                        }
                    }
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(ungroupedShapes, jsonOptions);

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Exported {ungroupedShapes.Count} ungrouped shape(s) to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple DTO for JSON serialization
    private class SimpleShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Page { get; set; } = string.Empty;
    }
}