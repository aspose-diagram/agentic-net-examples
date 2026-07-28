using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramFieldExport
{
    // Model representing a field inside a shape
    public class FieldInfo
    {
        public int IX { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public int Calendar { get; set; }
        public bool Deleted { get; set; }
    }

    // Model representing a shape and its collection of fields
    public class ShapeFieldInfo
    {
        public long ShapeId { get; set; }
        public string ShapeName { get; set; } = string.Empty;
        public List<FieldInfo> Fields { get; set; } = new();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output JSON file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramFieldExport <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            // Guard to ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputPath = args[1];

            // Container for all extracted shape field information
            var allShapeFields = new List<ShapeFieldInfo>();

            try
            {
                // Load the Visio diagram using Aspose.Diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes without any fields
                        if (shape.Fields == null || shape.Fields.Count == 0)
                            continue;

                        var shapeInfo = new ShapeFieldInfo
                        {
                            ShapeId = shape.ID,
                            ShapeName = shape.Name ?? string.Empty
                        };

                        // Extract each field's details
                        foreach (Field field in shape.Fields)
                        {
                            var fieldInfo = new FieldInfo
                            {
                                IX = field.IX,
                                // Cast the enum value to its underlying int representation
                                Type = (int)field.Type.Value,
                                Value = field.Value?.Val ?? string.Empty,
                                Format = field.Format?.Val ?? string.Empty,
                                // Convert CalendarValue enum to int, defaulting to 0 if null
                                Calendar = field.Calendar != null ? (int)field.Calendar.Value : 0,
                                Deleted = field.Del == BOOL.True
                            };

                            shapeInfo.Fields.Add(fieldInfo);
                        }

                        allShapeFields.Add(shapeInfo);
                    }
                }

                // Serialize the collected information to JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(allShapeFields, jsonOptions);

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Export completed. JSON saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}