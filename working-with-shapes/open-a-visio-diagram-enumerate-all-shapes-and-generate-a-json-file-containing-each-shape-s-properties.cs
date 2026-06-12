using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioShapeExport
{
    // DTO to hold shape information for JSON serialization
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
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioShapeExport <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                var shapesList = new List<ShapeInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Gather required properties
                        var info = new ShapeInfo
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
                            Text = shape.Text.Value.Text,
                            IsDeleted = shape.Del == BOOL.True
                        };

                        shapesList.Add(info);
                    }
                }

                // Serialize the list to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(shapesList, jsonOptions);

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Export completed. JSON saved to: {outputPath}");
            }
        }
    }
}