using System;
using System.IO;
using Aspose.Diagram;

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
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page.
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No non‑deleted shapes found on the first page.");
                    return;
                }

                // Enable gradient fill and set a gradient pattern.
                targetShape.Fill.FillPattern.Value = 25; // Gradient fill pattern.
                targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Clear any existing gradient stops.
                targetShape.Fill.GradientFill.GradientStops.Clear();

                // Add a new gradient stop at position 0.25 with yellow color (RGB 255,255,0).
                // Position uses MeasureConst.NUM (0‑1 range). Color uses a hex string and MeasureConst.Undefined.
                targetShape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0.25, MeasureConst.NUM),
                    new ColorValue("#FFFF00", MeasureConst.Undefined));

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}