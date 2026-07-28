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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a gradient fill defined
                    if (shape.Fill?.GradientFill != null && shape.Fill.GradientFill.GradientStops.Count > 0)
                    {
                        // Preserve existing stops
                        var existingStops = new System.Collections.Generic.List<(double position, string color)>();
                        foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                        {
                            double pos = stop.Position.Value;
                            string col = stop.Color.Value;
                            existingStops.Add((pos, col));
                        }

                        // Clear current stops
                        shape.Fill.GradientFill.GradientStops.Clear();

                        // Re‑add stops with shifted positions (+0.1) and clamp to 0‑1 range
                        foreach (var (position, color) in existingStops)
                        {
                            double newPos = position + 0.1;
                            if (newPos > 1.0) newPos = 1.0; // clamp upper bound
                            // Create new stop entries
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(newPos, MeasureConst.NUM),
                                new ColorValue(color, MeasureConst.Undefined));
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
