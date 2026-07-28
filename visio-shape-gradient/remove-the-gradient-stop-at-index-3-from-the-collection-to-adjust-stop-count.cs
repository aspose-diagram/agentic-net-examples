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
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page
            Page page = diagram.Pages[0];

            // Find a shape that has at least 4 gradient stops
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Fill != null &&
                    shape.Fill.GradientFill != null &&
                    shape.Fill.GradientFill.GradientStops.Count > 3)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape with enough gradient stops found.");
                return;
            }

            // Access the gradient fill of the shape
            GradientFill gradientFill = targetShape.Fill.GradientFill;

            // Collect all stops except the one at index 3
            List<GradientStop> keepers = new List<GradientStop>();
            int currentIndex = 0;
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                if (currentIndex != 3)
                {
                    keepers.Add(stop);
                }
                currentIndex++;
            }

            // Clear the existing stops
            gradientFill.GradientStops.Clear();

            // Re‑add the kept stops
            foreach (GradientStop stop in keepers)
            {
                // Preserve original position and color
                gradientFill.GradientStops.Add(stop.Position, stop.Color);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Gradient stop at index 3 removed and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
