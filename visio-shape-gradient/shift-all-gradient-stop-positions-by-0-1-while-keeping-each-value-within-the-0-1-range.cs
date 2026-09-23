using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a gradient fill enabled
                    if (shape.Fill != null &&
                        shape.Fill.GradientFill != null &&
                        shape.Fill.GradientFill.GradientEnabled != null &&
                        shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        var gradientFill = shape.Fill.GradientFill;
                        var originalStops = new System.Collections.Generic.List<GradientStop>();

                        // Collect existing stops
                        foreach (GradientStop stop in gradientFill.GradientStops)
                        {
                            originalStops.Add(stop);
                        }

                        // Clear existing stops
                        gradientFill.GradientStops.Clear();

                        // Re‑add stops with shifted positions
                        foreach (GradientStop stop in originalStops)
                        {
                            double originalPos = stop.Position.Value;
                            double shiftedPos = originalPos + 0.1;

                            // Keep the position within the 0‑1 range (clamp to 1.0)
                            if (shiftedPos > 1.0)
                                shiftedPos = 1.0;

                            // Preserve the original color
                            string colorHex = stop.Color.Value;

                            // Add the new stop
                            gradientFill.GradientStops.Add(
                                new DoubleValue(shiftedPos, MeasureConst.NUM),
                                new ColorValue(colorHex, MeasureConst.Undefined));
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
