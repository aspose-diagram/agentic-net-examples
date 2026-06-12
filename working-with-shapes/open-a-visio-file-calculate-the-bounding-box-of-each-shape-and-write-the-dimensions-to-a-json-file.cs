using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioBoundingBoxExporter
{
    // Simple DTO for JSON serialization
    public class ShapeInfo
    {
        public string PageName { get; set; }
        public long ShapeId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument) or default
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output JSON file path (second argument) or default
                string outputPath = args.Length > 1 ? args[1] : "shapes-bounding-box.json";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                var shapesInfo = new List<ShapeInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height from the shape's XForm cell collection
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        shapesInfo.Add(new ShapeInfo
                        {
                            PageName = page.Name,
                            ShapeId = shape.ID,
                            Width = width,
                            Height = height
                        });
                    }
                }

                // Serialize the list to JSON with indentation for readability
                string json = JsonSerializer.Serialize(shapesInfo, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Bounding box data for {shapesInfo.Count} shapes written to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}