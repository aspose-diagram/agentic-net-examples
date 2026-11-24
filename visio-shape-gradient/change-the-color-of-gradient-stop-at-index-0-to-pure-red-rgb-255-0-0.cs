using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output path (second argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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

            // Get the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Get the first shape on the page (index 0)
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }
            Shape shape = page.Shapes[0];

            // Ensure the shape uses a gradient fill
            shape.Fill.FillPattern.Value = 25; // 25 corresponds to gradient fill
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Clear existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at position 0 with pure red color (#FF0000)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // (Optional) Add a second stop to keep the gradient visible; using white as example
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#FFFFFF", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}