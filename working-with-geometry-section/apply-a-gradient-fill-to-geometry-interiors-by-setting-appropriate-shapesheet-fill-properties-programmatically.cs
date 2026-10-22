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
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply a gradient fill to the shape.
                    // 1. Set fill pattern to gradient (value 25).
                    shape.Fill.FillPattern.Value = 25;

                    // 2. Enable gradient fill.
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                    // 3. Set gradient direction (0 = horizontal, for example).
                    shape.Fill.GradientFill.GradientDir.Value = 0;

                    // 4. Clear any existing gradient stops.
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // 5. Add new gradient stops.
                    //    First stop at position 0 (start) with blue color.
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#0000FF", MeasureConst.Undefined));

                    //    Second stop at position 1 (end) with green color.
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