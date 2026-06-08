using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (provide via command line or use defaults)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a gradient fill enabled
                    if (shape.Fill?.GradientFill?.GradientEnabled?.Value != BOOL.True)
                        continue;

                    GradientFill gradientFill = shape.Fill.GradientFill;

                    // Collect shifted gradient stops
                    List<(double position, ColorValue color)> shiftedStops = new List<(double, ColorValue)>();

                    foreach (GradientStop stop in gradientFill.GradientStops)
                    {
                        double originalPos = stop.Position.Value;
                        double newPos = originalPos + 0.1;

                        // Clamp the position to the 0‑1 range
                        if (newPos > 1.0) newPos = 1.0;
                        if (newPos < 0.0) newPos = 0.0;

                        shiftedStops.Add((newPos, stop.Color));
                    }

                    // Replace existing stops with the shifted ones
                    gradientFill.GradientStops.Clear();
                    foreach (var (pos, col) in shiftedStops)
                    {
                        gradientFill.GradientStops.Add(
                            new DoubleValue(pos, MeasureConst.NUM),
                            col);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
