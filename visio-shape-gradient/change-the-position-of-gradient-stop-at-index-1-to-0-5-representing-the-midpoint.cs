using System.IO;
using System;
using System.Collections.Generic;
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

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Ensure the shape has a gradient fill
            if (shape != null && shape.Fill != null && shape.Fill.GradientFill != null)
            {
                var gradientFill = shape.Fill.GradientFill;

                // Preserve existing gradient stops
                var stops = new List<(double Position, string Color)>();
                foreach (GradientStop stop in gradientFill.GradientStops)
                {
                    stops.Add((stop.Position.Value, stop.Color.Value));
                }

                // Change the position of the stop at index 1 to 0.5 (midpoint) if it exists
                if (stops.Count > 1)
                {
                    stops[1] = (0.5, stops[1].Color);
                }

                // Re‑apply the gradient stops
                gradientFill.GradientStops.Clear();
                foreach (var s in stops)
                {
                    gradientFill.GradientStops.Add(
                        new DoubleValue(s.Position, MeasureConst.NUM),
                        new ColorValue(s.Color, MeasureConst.Undefined));
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
