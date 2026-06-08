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

            // Retrieve the first page (index 0) and a shape with ID 1
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Ensure the shape uses a gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Store existing gradient stops, modifying the stop at index 1 to pure green
            var updatedStops = new System.Collections.Generic.List<(double position, string color)>();

            int stopIndex = 0;
            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
            {
                double pos = stop.Position.Value;
                string col = stop.Color.Value;

                if (stopIndex == 1)
                {
                    // Change color to pure green (RGB 0,255,0) using hex notation
                    col = "#00FF00";
                }

                updatedStops.Add((pos, col));
                stopIndex++;
            }

            // Clear existing stops and re-add them with the updated colors
            shape.Fill.GradientFill.GradientStops.Clear();

            foreach (var (position, color) in updatedStops)
            {
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(position, MeasureConst.NUM),
                    new ColorValue(color, MeasureConst.Undefined));
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
