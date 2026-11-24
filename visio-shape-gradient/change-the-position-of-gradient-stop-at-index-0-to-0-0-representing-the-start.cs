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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a gradient fill with stops.
                        if (shape.Fill?.GradientFill?.GradientStops == null)
                            continue;

                        var gradientStops = shape.Fill.GradientFill.GradientStops;

                        // If there are no stops, nothing to modify.
                        if (gradientStops.Count == 0)
                            continue;

                        // Preserve existing stop data while modifying the first stop's position.
                        List<(double position, string color)> stopData = new List<(double, string)>();
                        int index = 0;
                        foreach (GradientStop stop in gradientStops)
                        {
                            double pos = stop.Position.Value;
                            string col = stop.Color.Value;
                            // Change position of the stop at index 0 to 0.0.
                            if (index == 0)
                                pos = 0.0;
                            stopData.Add((pos, col));
                            index++;
                        }

                        // Clear existing stops before re‑adding.
                        gradientStops.Clear();

                        // Re‑add stops with the (potentially) updated position for index 0.
                        foreach (var (position, color) in stopData)
                        {
                            gradientStops.Add(
                                new DoubleValue(position, MeasureConst.NUM),
                                new ColorValue(color, MeasureConst.Undefined));
                        }

                        // Refresh shape data to apply changes.
                        shape.RefreshData();
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}