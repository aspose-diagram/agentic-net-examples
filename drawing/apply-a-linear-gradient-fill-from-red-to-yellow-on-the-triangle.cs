using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply linear gradient fill from red to yellow.
                        // Set fill pattern to gradient (value 25).
                        shape.Fill.FillPattern.Value = 25;

                        // Enable gradient fill.
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                        // Set gradient direction (0 = left‑to‑right).
                        shape.Fill.GradientFill.GradientDir.Value = 0;

                        // Clear any existing gradient stops.
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Add gradient stop at position 0 (red).
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0, MeasureConst.NUM),
                            new ColorValue("#FF0000", MeasureConst.Undefined));

                        // Add gradient stop at position 1 (yellow).
                        shape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(1, MeasureConst.NUM),
                            new ColorValue("#FFFF00", MeasureConst.Undefined));
                    }
                }

                // Save the modified diagram to the specified output path as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}