using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // DTO to hold line and fill definitions
    private class ShapeStyle
    {
        public string LineColor { get; set; }
        public double LineWeight { get; set; }
        public string LinePattern { get; set; }
        public int BeginArrow { get; set; }
        public int EndArrow { get; set; }

        public string FillForegnd { get; set; }
        public string FillBkgnd { get; set; }
        public int FillPattern { get; set; }
        public double FillForegndTrans { get; set; }
        public double FillBkgndTrans { get; set; }
    }

    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output JSON file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "shapeStyle.json";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Get the first shape (ID 1 is typical, but we use the first in collection)
            Shape shape = page.Shapes.GetShape(1);

            // Extract line properties
            string lineColor = shape.Line.LineColor.Value;
            double lineWeight = shape.Line.LineWeight.Value;
            string linePattern = shape.Line.LinePattern.Value.ToString();
            int beginArrow = (int)shape.Line.BeginArrow.Value;
            int endArrow = (int)shape.Line.EndArrow.Value;

            // Extract fill properties
            string fillForegnd = shape.Fill.FillForegnd.Value;
            string fillBkgnd = shape.Fill.FillBkgnd.Value;
            int fillPattern = shape.Fill.FillPattern.Value;
            double fillForegndTrans = shape.Fill.FillForegndTrans.Value;
            double fillBkgndTrans = shape.Fill.FillBkgndTrans.Value;

            // Populate DTO
            ShapeStyle style = new ShapeStyle
            {
                LineColor = lineColor,
                LineWeight = lineWeight,
                LinePattern = linePattern,
                BeginArrow = beginArrow,
                EndArrow = endArrow,
                FillForegnd = fillForegnd,
                FillBkgnd = fillBkgnd,
                FillPattern = fillPattern,
                FillForegndTrans = fillForegndTrans,
                FillBkgndTrans = fillBkgndTrans
            };

            // Serialize to JSON with indentation
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(style, jsonOptions);

            // Write JSON to output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Shape style exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}