using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramShapeExport
{
    // DTO for JSON output
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameU { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output JSON file path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramShapeExport <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            // Guard: ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputPath = args[1];

            // Load the diagram inside a try/catch to capture loading errors
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            var ungroupedShapes = new List<ShapeInfo>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that belong to a group
                    if (shape.IsInGroup())
                        continue;

                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Collect basic information about the shape
                    var info = new ShapeInfo
                    {
                        Id = shape.ID,
                        Name = shape.Name ?? string.Empty,
                        NameU = shape.NameU ?? string.Empty,
                        // Type is an enum (TypeValue); convert to string directly
                        Type = shape.Type.ToString()
                    };
                    ungroupedShapes.Add(info);
                }
            }

            // Serialize the list to JSON with indentation
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ungroupedShapes, jsonOptions);

            // Write JSON to the output file inside a try/catch to capture I/O errors
            try
            {
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Exported {ungroupedShapes.Count} ungrouped shapes to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write JSON file: {ex.Message}");
            }
        }
    }
}