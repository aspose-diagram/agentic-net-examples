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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape to modify (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape has a gradient fill enabled
            shape.Fill.FillPattern.Value = 25;                     // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Reference to the gradient fill collection
            var gradientFill = shape.Fill.GradientFill;

            // Collect all gradient stops except the one at index 2
            var keptStops = new List<GradientStop>();
            int index = 0;
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                if (index != 2)
                {
                    keptStops.Add(stop);
                }
                index++;
            }

            // Clear the existing stops
            gradientFill.GradientStops.Clear();

            // Re‑add the kept stops
            foreach (GradientStop stop in keptStops)
            {
                gradientFill.GradientStops.Add(
                    new DoubleValue(stop.Position.Value, MeasureConst.NUM),
                    new ColorValue(stop.Color.Value, MeasureConst.Undefined));
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
