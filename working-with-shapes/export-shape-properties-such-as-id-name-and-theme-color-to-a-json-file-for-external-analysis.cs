using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramExport
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
        public static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramExport <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            var shapeInfos = new List<ShapeInfo>();

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve theme color (foreground fill color) if available
                    string color = null;
                    if (shape.Fill != null && shape.Fill.FillForegnd != null && shape.Fill.FillForegnd.Value != null)
                    {
                        color = shape.Fill.FillForegnd.Value;
                    }

                    shapeInfos.Add(new ShapeInfo
                    {
                        ID = shape.ID,
                        Name = shape.Name,
                        ThemeColor = color
                    });
                }
            }

            // Serialize to JSON with indentation
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(shapeInfos, options);

            // Write JSON to file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Export completed. {shapeInfos.Count} shapes written to '{outputPath}'.");
        }
    }
}