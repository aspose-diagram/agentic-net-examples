using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeStyleExport
{
    // DTO for serializing line and fill properties
    public class ShapeStyle
    {
        public string LineColor { get; set; }
        public double LineWeight { get; set; }
        public int LinePattern { get; set; }
        public string FillForegnd { get; set; }
        public string FillBkgnd { get; set; }
        public int FillPattern { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape on the page
                Shape shape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.Del == BOOL.False)
                    {
                        shape = s;
                        break;
                    }
                }

                if (shape == null)
                    throw new Exception("No visible shape found on the first page.");

                // Extract line properties
                string lineColor = shape.Line.LineColor.Value;
                double lineWeight = shape.Line.LineWeight.Value;
                int linePattern = (int)shape.Line.LinePattern.Value; // enum to int

                // Extract fill properties
                string fillForegnd = shape.Fill.FillForegnd.Value;
                string fillBkgnd = shape.Fill.FillBkgnd.Value;
                int fillPattern = shape.Fill.FillPattern.Value;

                // Create DTO instance
                ShapeStyle style = new ShapeStyle
                {
                    LineColor = lineColor,
                    LineWeight = lineWeight,
                    LinePattern = linePattern,
                    FillForegnd = fillForegnd,
                    FillBkgnd = fillBkgnd,
                    FillPattern = fillPattern
                };

                // Serialize to JSON with indentation
                string json = JsonSerializer.Serialize(style, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText("shapeStyle.json", json);

                Console.WriteLine("Shape style exported to shapeStyle.json");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}