using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeExportExample
{
    // DTO to hold shape information for JSON serialization
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ThemeColor { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output JSON file path
                string outputPath = "shapes.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // List to collect shape information
                List<ShapeInfo> shapeInfos = new List<ShapeInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve ID and Name
                        long id = shape.ID;
                        string name = shape.Name;

                        // Retrieve theme color (using fill foreground color as an example)
                        // Ensure the Fill and FillForegnd cells are present
                        string themeColor = shape.Fill?.FillForegnd?.Value ?? "#000000";

                        // Add to collection
                        shapeInfos.Add(new ShapeInfo
                        {
                            Id = id,
                            Name = name,
                            ThemeColor = themeColor
                        });
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(shapeInfos, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {shapeInfos.Count} shapes to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}