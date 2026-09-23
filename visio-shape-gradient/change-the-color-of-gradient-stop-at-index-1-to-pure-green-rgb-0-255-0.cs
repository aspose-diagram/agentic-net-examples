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

            // Get the first page and a shape (replace 1 with the actual shape ID if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Ensure the shape has a gradient fill enabled
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (optional)

            // Collect existing gradient stops
            List<GradientStop> stops = new List<GradientStop>();
            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
            {
                stops.Add(stop);
            }

            // Change the color of the gradient stop at index 1 to pure green (#00FF00)
            if (stops.Count > 1)
            {
                GradientStop targetStop = stops[1];
                // Replace the color with green
                targetStop.Color = new ColorValue("#00FF00", MeasureConst.Undefined);
            }

            // Rebuild the gradient stops collection with the updated colors
            shape.Fill.GradientFill.GradientStops.Clear();
            foreach (GradientStop stop in stops)
            {
                shape.Fill.GradientFill.GradientStops.Add(
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
