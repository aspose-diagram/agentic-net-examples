using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeStyleExport
{
    // DTO for serializing line and fill definitions
    public class ShapeStyle
    {
        public LineStyle Line { get; set; } = new();
        public FillStyle Fill { get; set; } = new();

        public class LineStyle
        {
            public string Color { get; set; } = "";
            public double Weight { get; set; }
            public int Pattern { get; set; }
            public int BeginArrow { get; set; }
            public int EndArrow { get; set; }
        }

        public class FillStyle
        {
            public string ForegroundColor { get; set; } = "";
            public string BackgroundColor { get; set; } = "";
            public int Pattern { get; set; }
            public double ForegroundTransparency { get; set; }
            public double BackgroundTransparency { get; set; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No suitable shape found.");
                    return;
                }

                // Extract line properties
                var line = new ShapeStyle.LineStyle
                {
                    Color = targetShape.Line.LineColor.Value,
                    Weight = targetShape.Line.LineWeight.Value,
                    Pattern = (int)targetShape.Line.LinePattern.Value,
                    BeginArrow = (int)targetShape.Line.BeginArrow.Value,
                    EndArrow = (int)targetShape.Line.EndArrow.Value
                };

                // Extract fill properties
                var fill = new ShapeStyle.FillStyle
                {
                    ForegroundColor = targetShape.Fill.FillForegnd.Value,
                    BackgroundColor = targetShape.Fill.FillBkgnd.Value,
                    Pattern = targetShape.Fill.FillPattern.Value,
                    ForegroundTransparency = targetShape.Fill.FillForegndTrans.Value,
                    BackgroundTransparency = targetShape.Fill.FillBkgndTrans.Value
                };

                // Combine into a single style object
                var style = new ShapeStyle
                {
                    Line = line,
                    Fill = fill
                };

                // Serialize the style to JSON with indentation
                string json = JsonSerializer.Serialize(style, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to a file
                string jsonPath = "shapeStyle.json";
                File.WriteAllText(jsonPath, json);

                Console.WriteLine($"Shape style exported to {jsonPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}