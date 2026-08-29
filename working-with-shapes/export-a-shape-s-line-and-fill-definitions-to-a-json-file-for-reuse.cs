using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeStyleExport
{
    // DTO for line and fill properties
    public class ShapeStyle
    {
        public string LineColor { get; set; }
        public double LineWeight { get; set; }
        public int LinePattern { get; set; }
        public int BeginArrow { get; set; }
        public int EndArrow { get; set; }

        public string FillForegnd { get; set; }
        public string FillBkgnd { get; set; }
        public int FillPattern { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ShapeStyleExport <inputVisioFile> <outputJsonFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the first non‑deleted shape on the first page
            Page page = diagram.Pages[0];
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
                Console.WriteLine("No visible shape found in the diagram.");
                return;
            }

            // Extract line properties
            var line = targetShape.Line;
            var fill = targetShape.Fill;

            ShapeStyle style = new ShapeStyle
            {
                LineColor = line.LineColor.Value,
                LineWeight = line.LineWeight.Value,
                LinePattern = (int)line.LinePattern.Value,
                BeginArrow = (int)line.BeginArrow.Value,
                EndArrow = (int)line.EndArrow.Value,

                FillForegnd = fill.FillForegnd.Value,
                FillBkgnd = fill.FillBkgnd.Value,
                FillPattern = (int)fill.FillPattern.Value
            };

            // Serialize to JSON with indentation
            string json = JsonSerializer.Serialize(style, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Shape style exported to '{outputPath}'.");
        }
    }
}