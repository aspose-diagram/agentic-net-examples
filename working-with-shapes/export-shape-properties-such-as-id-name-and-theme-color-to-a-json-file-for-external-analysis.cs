using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramShapeExport
{
    // DTO for JSON serialization
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ThemeColor { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Output JSON file path
                string jsonOutputPath = "shape_properties.json";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // List to hold shape information
                List<ShapeInfo> shapesInfo = new List<ShapeInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve fill foreground color as theme color (hex string)
                        string themeColor = shape.Fill.FillForegnd.Value;

                        // Create DTO and add to list
                        ShapeInfo info = new ShapeInfo
                        {
                            Id = shape.ID,
                            Name = shape.Name,
                            ThemeColor = themeColor
                        };
                        shapesInfo.Add(info);
                    }
                }

                // Serialize to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(shapesInfo, jsonOptions);

                // Write JSON to file
                File.WriteAllText(jsonOutputPath, json);

                Console.WriteLine($"Exported {shapesInfo.Count} shape(s) to '{jsonOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}