using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeFieldExport
{
    // DTO for a field within a shape
    public class FieldInfo
    {
        public int IX { get; set; }
        public int Type { get; set; }          // Underlying integer value of TypeFieldValue enum
        public string? Value { get; set; }     // Field value as string
        public string? Format { get; set; }    // Field format string
    }

    // DTO for a shape and its collection of fields
    public class ShapeFieldInfo
    {
        public long ShapeId { get; set; }
        public string? Name { get; set; }
        public List<FieldInfo>? Fields { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string jsonOutputPath = "shape_fields.json";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // List to hold extracted information
                List<ShapeFieldInfo> extractedData = new List<ShapeFieldInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Only process shapes that contain fields
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            ShapeFieldInfo shapeInfo = new ShapeFieldInfo
                            {
                                ShapeId = shape.ID,
                                Name = shape.Name,
                                Fields = new List<FieldInfo>()
                            };

                            foreach (Field field in shape.Fields)
                            {
                                FieldInfo fi = new FieldInfo
                                {
                                    IX = field.IX,
                                    // Cast enum to its underlying int value
                                    Type = (int)field.Type.Value,
                                    Value = field.Value.Val,
                                    Format = field.Format.Val
                                };
                                shapeInfo.Fields.Add(fi);
                            }

                            extractedData.Add(shapeInfo);
                        }
                    }
                }

                // Serialize the list to JSON with indentation for readability
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(extractedData, options);

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