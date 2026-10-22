using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

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
                    // Preserve existing paragraph and character formatting by NOT modifying shape.Paras or shape.Chars

                    // Apply gradient fill
                    // Set fill pattern to gradient (value 25)
                    shape.Fill.FillPattern.Value = 25;

                    // Enable gradient fill
                    shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                    // Set gradient direction (0 = horizontal)
                    shape.Fill.GradientFill.GradientDir.Value = 0;

                    // Clear any existing gradient stops
                    shape.Fill.GradientFill.GradientStops.Clear();

                    // Add new gradient stops (blue to green)
                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(0, MeasureConst.NUM),
                        new ColorValue("#0000FF", MeasureConst.Undefined));

                    shape.Fill.GradientFill.GradientStops.Add(
                        new DoubleValue(1, MeasureConst.NUM),
                        new ColorValue("#00FF00", MeasureConst.Undefined));
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}