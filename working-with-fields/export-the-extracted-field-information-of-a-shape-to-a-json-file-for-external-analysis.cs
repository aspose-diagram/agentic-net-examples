using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeFieldExport
{
    // DTO for individual field information
    public class FieldInfo
    {
        public int IX { get; set; }
        public int Type { get; set; }          // Underlying enum value
        public string Value { get; set; }      // field.Value.Val
        public string Format { get; set; }     // field.Format.Val
    }

    // DTO for shape information including its fields
    public class ShapeFieldInfo
    {
        public long ShapeId { get; set; }
        public string Name { get; set; }
        public string NameU { get; set; }
        public List<FieldInfo> Fields { get; set; } = new();
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Output JSON file path
                string jsonOutputPath = "shape_fields.json";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // List to hold extracted information
                List<ShapeFieldInfo> shapeData = new();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Prepare shape info container
                        ShapeFieldInfo shapeInfo = new()
                        {
                            ShapeId = shape.ID,
                            Name = shape.Name,
                            NameU = shape.NameU
                        };

                        // Extract fields if any exist
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            foreach (Field field in shape.Fields)
                            {
                                FieldInfo fInfo = new()
                                {
                                    IX = field.IX,
                                    Type = (int)field.Type.Value,
                                    Value = field.Value?.Val,
                                    Format = field.Format?.Val
                                };
                                shapeInfo.Fields.Add(fInfo);
                            }
                        }

                        shapeData.Add(shapeInfo);
                    }
                }

                // Serialize to JSON with indentation for readability
                JsonSerializerOptions options = new()
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(shapeData, options);

                // Write JSON to file
                File.WriteAllText(jsonOutputPath, json);

                Console.WriteLine($"Export completed. JSON saved to: {jsonOutputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}