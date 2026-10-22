using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page.
                Shape targetShape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.Del == BOOL.False)
                    {
                        targetShape = s;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No visible shape found on the first page.");
                    return;
                }

                // Enable gradient fill for the shape.
                targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Access the gradient stops collection.
                var gradientStops = targetShape.Fill.GradientFill.GradientStops;

                // Ensure there are at least two stops to modify index 1.
                if (gradientStops.Count < 2)
                {
                    Console.Error.WriteLine("The shape does not have a gradient stop at index 1.");
                    return;
                }

                // Store existing positions and colors.
                List<double> positions = new List<double>();
                List<ColorValue> colors = new List<ColorValue>();
                foreach (GradientStop stop in gradientStops)
                {
                    positions.Add(stop.Position.Value);
                    colors.Add(stop.Color); // Store the ColorValue object itself.
                }

                // Clear existing stops before re‑adding.
                gradientStops.Clear();

                // Re‑add stops, adjusting the position of the stop at index 1 to 0.5.
                for (int i = 0; i < positions.Count; i++)
                {
                    double newPos = (i == 1) ? 0.5 : positions[i];
                    gradientStops.Add(new DoubleValue(newPos, MeasureConst.NUM), colors[i]);
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
