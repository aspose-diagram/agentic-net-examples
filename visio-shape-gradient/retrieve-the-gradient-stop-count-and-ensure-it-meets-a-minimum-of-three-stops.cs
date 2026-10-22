using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) – fallback to a default name.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output file path (second argument) – fallback to a default name.
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Access the gradient fill of the shape.
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Retrieve the current gradient stop count.
            int stopCount = gradientFill.GradientStops.Count;
            Console.WriteLine($"Current gradient stop count: {stopCount}");

            // Ensure there are at least three gradient stops.
            if (stopCount < 3)
            {
                // Enable gradient if it is not already enabled.
                gradientFill.GradientEnabled.Value = BOOL.True;

                // Clear existing stops and add three default stops.
                gradientFill.GradientStops.Clear();

                // Add stop at position 0 (red).
                gradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined));

                // Add stop at position 0.5 (green).
                gradientFill.GradientStops.Add(
                    new DoubleValue(0.5, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Add stop at position 1 (blue).
                gradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined));

                Console.WriteLine("Added missing gradient stops to meet the minimum of three.");
            }
            else
            {
                Console.WriteLine("The shape already has three or more gradient stops.");
            }

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}