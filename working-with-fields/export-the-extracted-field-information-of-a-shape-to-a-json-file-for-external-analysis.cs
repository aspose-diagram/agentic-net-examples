using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramFieldExport
{
    // DTO for field information
    public class FieldInfo
    {
        public int IX { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public int Type { get; set; }
    }

    // DTO for shape information
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string NameU { get; set; }
        public List<FieldInfo> Fields { get; set; } = new();
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (adjust the path as needed)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Collect shape information
                List<ShapeInfo> shapesData = new();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        ShapeInfo shapeInfo = new ShapeInfo
                        {
                            Id = shape.ID,
                            NameU = shape.NameU
                        };

                        // Extract fields (text insertion fields) if any
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            foreach (Field field in shape.Fields)
                            {
                                // Field.Value is read‑only; use its .Val property for the actual string
                                string fieldValue = field.Value?.Val ?? string.Empty;
                                string fieldFormat = field.Format?.Val ?? string.Empty;
                                int fieldType = (int)field.Type?.Value;

                                shapeInfo.Fields.Add(new FieldInfo
                                {
                                    IX = field.IX,
                                    Value = fieldValue,
                                    Format = fieldFormat,
                                    Type = fieldType
                                });
                            }
                        }

                        shapesData.Add(shapeInfo);
                    }
                }

                // Serialize to JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonOutput = JsonSerializer.Serialize(shapesData, jsonOptions);

                // Write JSON to file
                string outputPath = "shape_fields.json";
                File.WriteAllText(outputPath, jsonOutput);

                Console.WriteLine($"Export completed. JSON saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}