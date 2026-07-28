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
        public long ID { get; set; }
        public string Name { get; set; }
        public string ThemeColor { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path for the output JSON file
                const string outputPath = "shapes.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // List to hold extracted shape information
                List<ShapeInfo> shapesData = new List<ShapeInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve ID, Name, and theme color (foreground fill)
                        long id = shape.ID;
                        string name = shape.Name ?? string.Empty;
                        string themeColor = shape.Fill?.FillForegnd?.Value ?? string.Empty;

                        shapesData.Add(new ShapeInfo
                        {
                            ID = id,
                            Name = name,
                            ThemeColor = themeColor
                        });
                    }
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(shapesData, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {shapesData.Count} shapes to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}