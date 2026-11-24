using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply a gradient fill to the shape.
                    // Set fill pattern to gradient (value 25 as per Visio spec).
                    shape.Fill.FillPattern.Value = 25;

                    // Enable gradient fill.
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                    // Set gradient direction (0 = horizontal, adjust as needed).
                    shape.Fill.GradientFill.GradientDir.Value = 0;

                    // Clear any existing gradient stops.
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Add a start stop (position 0) with red color.
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#FF0000", MeasureConst.Undefined));

                    // Add an end stop (position 1) with green color.
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#00FF00", MeasureConst.Undefined));
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}