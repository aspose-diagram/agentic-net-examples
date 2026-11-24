using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use arguments if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether a removal was performed
            BOOL removalDone = BOOL.False;

            // Iterate through pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a gradient fill
                    if (shape.Fill != null && shape.Fill.GradientFill != null)
                    {
                        GradientFill gradientFill = shape.Fill.GradientFill;

                        // Check if there are enough stops to remove index 3
                        if (gradientFill.GradientStops != null && gradientFill.GradientStops.Count > 3)
                        {
                            // Collect gradient stops except the one at index 3
                            List<GradientStop> keptStops = new List<GradientStop>();
                            int currentIndex = 0;
                            foreach (GradientStop stop in gradientFill.GradientStops)
                            {
                                if (currentIndex != 3)
                                {
                                    keptStops.Add(stop);
                                }
                                currentIndex++;
                            }

                            // Clear existing stops
                            gradientFill.GradientStops.Clear();

                            // Re-add the kept stops
                            foreach (GradientStop keep in keptStops)
                            {
                                double position = keep.Position.Value;
                                string colorHex = keep.Color.Value;

                                gradientFill.GradientStops.Add(
                                    new DoubleValue(position, MeasureConst.NUM),
                                    new ColorValue(colorHex, MeasureConst.Undefined));
                            }

                            Console.WriteLine($"Removed gradient stop at index 3 from shape ID {shape.ID} on page ID {page.ID}.");
                            removalDone = BOOL.True;

                            // If only one removal is needed, exit loops
                            break;
                        }
                    }
                }

                if (removalDone == BOOL.True)
                    break;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}