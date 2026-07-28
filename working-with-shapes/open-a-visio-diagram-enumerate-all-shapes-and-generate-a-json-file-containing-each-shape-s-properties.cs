using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioShapeExport
{
    // Simple DTO to hold shape information for JSON serialization
    public class ShapeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameU { get; set; }
        public string Type { get; set; }
        public string MasterName { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle { get; set; }
        public string Text { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output JSON file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioShapeExport <inputVisioPath> <outputJsonPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect shape information from all pages
                List<ShapeInfo> shapesInfo = new List<ShapeInfo>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        ShapeInfo info = new ShapeInfo
                        {
                            Id = shape.ID,
                            Name = shape.Name,
                            NameU = shape.NameU,
                            Type = shape.Type.ToString(),
                            MasterName = shape.Master != null ? shape.Master.Name : null,
                            PinX = shape.XForm.PinX.Value,
                            PinY = shape.XForm.PinY.Value,
                            Width = shape.XForm.Width.Value,
                            Height = shape.XForm.Height.Value,
                            Angle = shape.XForm.Angle.Value,
                            Text = shape.Text.Value.Text
                        };

                        shapesInfo.Add(info);
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(shapesInfo, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Successfully exported {shapesInfo.Count} shapes to JSON file: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}