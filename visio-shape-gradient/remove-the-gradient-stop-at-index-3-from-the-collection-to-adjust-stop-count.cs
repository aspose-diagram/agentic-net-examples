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

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Ensure gradient fill is enabled (required before manipulating stops)
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Reference to the gradient fill object
            var gradientFill = shape.Fill.GradientFill;

            // Collect gradient stops except the one at index 3
            var keptStops = new List<(double Position, string Color)>();
            int currentIndex = 0;
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                if (currentIndex != 3) // Skip the stop at index 3
                {
                    double pos = stop.Position.Value;
                    string col = stop.Color.Value;
                    keptStops.Add((pos, col));
                }
                currentIndex++;
            }

            // Clear all existing stops
            gradientFill.GradientStops.Clear();

            // Re‑add the retained stops
            foreach (var kv in keptStops)
            {
                gradientFill.GradientStops.Add(
                    new DoubleValue(kv.Position, MeasureConst.NUM),
                    new ColorValue(kv.Color, MeasureConst.Undefined));
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
