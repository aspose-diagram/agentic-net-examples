using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
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

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Preserve existing text formatting – do not modify Paras or Chars

                    // Apply a gradient fill
                    // Set fill pattern to gradient (value 25)
                    shape.Fill.FillPattern.Value = 25;

                    // Enable gradient fill
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                    // Set gradient direction (0 = horizontal)
                    shape.Fill.GradientFill.GradientDir.Value = 0;

                    // Clear any existing gradient stops
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Add new gradient stops (red at start, green at end)
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#FF0000", MeasureConst.Undefined));

                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#00FF00", MeasureConst.Undefined));
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}