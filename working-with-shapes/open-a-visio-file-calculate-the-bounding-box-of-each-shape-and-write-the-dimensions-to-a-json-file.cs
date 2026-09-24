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
        public int PageIndex { get; set; }
        public long ShapeId { get; set; }
        public string Name { get; set; }
        public string NameU { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioBoundingBoxExporter <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            var shapesInfo = new List<ShapeInfo>();
            int pageIndex = 0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    var info = new ShapeInfo
                    {
                        PageIndex = pageIndex,
                        ShapeId = shape.ID,
                        Name = shape.Name,
                        NameU = shape.NameU,
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value,
                        Width = shape.XForm.Width.Value,
                        Height = shape.XForm.Height.Value
                    };

                    shapesInfo.Add(info);
                }

                pageIndex++;
            }

            // Serialize to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(shapesInfo, jsonOptions);

            // Write JSON to the specified file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Bounding box data for {shapesInfo.Count} shapes written to '{outputPath}'.");
        }
    }
}