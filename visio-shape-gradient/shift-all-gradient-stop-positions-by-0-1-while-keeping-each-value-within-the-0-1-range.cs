using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Access the gradient fill of the shape
                    var gradientFill = shape.Fill.GradientFill;

                    // Proceed only if there are gradient stops defined
                    if (gradientFill != null && gradientFill.GradientStops.Count > 0)
                    {
                        // Preserve existing stops (position and color)
                        var existingStops = new List<(double Position, string Color)>();

                        foreach (GradientStop stop in gradientFill.GradientStops)
                        {
                            double pos = stop.Position.Value;          // Current position (0‑1)
                            string col = stop.Color.Value;            // Hex color string
                            existingStops.Add((pos, col));
                        }

                        // Clear current stops
                        gradientFill.GradientStops.Clear();

                        // Re‑add stops with shifted positions, clamped to the 0‑1 range
                        foreach (var (pos, col) in existingStops)
                        {
                            double newPos = pos + 0.1;
                            if (newPos > 1.0) newPos = 1.0;
                            if (newPos < 0.0) newPos = 0.0;

                            gradientFill.GradientStops.Add(
                                new DoubleValue(newPos, MeasureConst.NUM),
                                new ColorValue(col, MeasureConst.Undefined));
                        }
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
