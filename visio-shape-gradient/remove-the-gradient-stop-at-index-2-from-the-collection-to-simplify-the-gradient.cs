using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Define output path
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a gradient fill
                    if (shape.Fill == null || shape.Fill.GradientFill == null)
                        continue;

                    GradientFill gradientFill = shape.Fill.GradientFill;

                    // Check if there are enough gradient stops to remove index 2
                    if (gradientFill.GradientStops == null || gradientFill.GradientStops.Count <= 2)
                        continue;

                    // Collect gradient stops except the one at index 2
                    List<GradientStop> keptStops = new List<GradientStop>();
                    for (int i = 0; i < gradientFill.GradientStops.Count; i++)
                    {
                        GradientStop stop = gradientFill.GradientStops[i];
                        if (i != 2) // Skip the stop at index 2
                        {
                            keptStops.Add(stop);
                        }
                    }

                    // Clear existing stops
                    gradientFill.GradientStops.Clear();

                    // Re-add the kept stops using the required Add method signature
                    foreach (GradientStop stop in keptStops)
                    {
                        // Preserve original position and color
                        DoubleValue position = new DoubleValue(stop.Position.Value, MeasureConst.NUM);
                        ColorValue color = new ColorValue(stop.Color.Value, MeasureConst.Undefined);
                        gradientFill.GradientStops.Add(position, color);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}