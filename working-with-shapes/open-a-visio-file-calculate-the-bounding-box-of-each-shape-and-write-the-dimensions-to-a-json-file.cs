using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioBoundingBoxExporter
{
    // Simple DTO to hold shape dimensions
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Determine input and output paths
            string inputPath;
            string outputPath;

            if (args.Length >= 2)
            {
                inputPath = args[0];
                outputPath = args[1];
            }
            else
            {
                Console.Write("Enter path to Visio file: ");
                inputPath = Console.ReadLine();

                Console.Write("Enter path for output JSON file: ");
                outputPath = Console.ReadLine();
            }

            // Validate input file existence
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"Visio file not found: {inputPath}");
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect shape information
            List<ShapeInfo> shapesInfo = new List<ShapeInfo>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    ShapeInfo info = new ShapeInfo
                    {
                        Id = shape.ID,
                        Name = shape.NameU,
                        Width = shape.XForm.Width.Value,
                        Height = shape.XForm.Height.Value,
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value
                    };

                    shapesInfo.Add(info);
                }
            }

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(shapesInfo, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Bounding box data for {shapesInfo.Count} shapes written to {outputPath}");
        }
    }
}