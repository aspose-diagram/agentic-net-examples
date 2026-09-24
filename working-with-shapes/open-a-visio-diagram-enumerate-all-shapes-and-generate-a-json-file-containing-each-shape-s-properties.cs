using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioShapeExport
{
    // DTO representing the properties we want to capture for each shape
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
        public Dictionary<string, string> CustomProperties { get; set; } = new Dictionary<string, string>();
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output JSON file path
                string outputPath = "shapes.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // List to hold shape information
                List<ShapeInfo> shapesInfo = new List<ShapeInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
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

                        // Capture custom shape properties (Props)
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (!string.IsNullOrEmpty(prop.Name))
                                {
                                    info.CustomProperties[prop.Name] = prop.Value.Val;
                                }
                            }
                        }

                        shapesInfo.Add(info);
                    }
                }

                // Serialize the list to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(shapesInfo, jsonOptions);

                // Write JSON to file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {shapesInfo.Count} shapes to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}