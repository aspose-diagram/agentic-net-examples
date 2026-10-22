using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find gradient fills.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is not deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Access the gradient fill object.
                    GradientFill gradientFill = shape.Fill?.GradientFill;
                    if (gradientFill == null)
                        continue;

                    // Proceed only if there are more than 3 stops.
                    if (gradientFill.GradientStops.Count <= 3)
                        continue;

                    // Collect all stops except the one at index 3.
                    List<GradientStop> keptStops = new List<GradientStop>();
                    for (int i = 0; i < gradientFill.GradientStops.Count; i++)
                    {
                        GradientStop stop = gradientFill.GradientStops[i];
                        if (i != 3)
                        {
                            keptStops.Add(stop);
                        }
                    }

                    // Clear existing stops and re‑add the kept ones.
                    gradientFill.GradientStops.Clear();
                    foreach (GradientStop stop in keptStops)
                    {
                        // Re‑create the stop using its original position and color.
                        DoubleValue position = new DoubleValue(stop.Position.Value, MeasureConst.NUM);
                        ColorValue color = new ColorValue(stop.Color.Value, MeasureConst.Undefined);
                        gradientFill.GradientStops.Add(position, color);
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}