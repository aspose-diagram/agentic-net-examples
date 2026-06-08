using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            Shape targetShape = null;

            foreach (Shape shape in page.Shapes)
            {
                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No shape found on the first page.");
                return;
            }

            // Enable gradient fill
            targetShape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient angle to 45 degrees for diagonal effect
            targetShape.Fill.GradientFill.GradientAngle.Value = 45;

            // Optionally clear existing stops and add two sample stops
            targetShape.Fill.GradientFill.GradientStops.Clear();
            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));
            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with gradient angle set to 45 degrees: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}