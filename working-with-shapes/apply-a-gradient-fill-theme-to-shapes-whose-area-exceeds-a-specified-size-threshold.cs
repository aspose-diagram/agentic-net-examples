using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, area threshold
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <areaThreshold>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string thresholdArg = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the area threshold
        if (!double.TryParse(thresholdArg, out double areaThreshold))
        {
            Console.Error.WriteLine($"Invalid area threshold: {thresholdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compute shape area using Width and Height values
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double area = width * height;

                    // Apply gradient fill only if area exceeds the threshold
                    if (area > areaThreshold)
                    {
                        // Set fill pattern to gradient (value 25)
                        shape.Fill.FillPattern.Value = 25;

                        // Enable gradient fill
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                        // Set gradient direction (0 = left‑to‑right)
                        shape.Fill.GradientFill.GradientDir.Value = 0;

                        // Clear any existing gradient stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add two gradient stops: red at start, green at end
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue("#FF0000", MeasureConst.Undefined));

                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(1, MeasureConst.NUM),
                            new ColorValue("#00FF00", MeasureConst.Undefined));
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}