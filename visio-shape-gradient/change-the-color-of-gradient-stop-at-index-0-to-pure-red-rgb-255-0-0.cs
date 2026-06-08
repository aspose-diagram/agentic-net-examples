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

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Get the first shape on the page (index 0)
            Shape shape = page.Shapes[0];

            // Ensure the shape uses a gradient fill
            shape.Fill.FillPattern.Value = 25;                         // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Enable gradient

            // Retrieve existing gradient stops
            GradientStopCollection stops = shape.Fill.GradientFill.GradientStops;
            List<GradientStop> existingStops = new List<GradientStop>();

            foreach (GradientStop stop in stops)
            {
                existingStops.Add(stop);
            }

            // Clear all existing stops
            stops.Clear();

            // Re‑add stops, changing the color of the stop at index 0 to pure red (#FF0000)
            for (int i = 0; i < existingStops.Count; i++)
            {
                GradientStop original = existingStops[i];
                double position = original.Position.Value;
                string colorHex = (i == 0) ? "#FF0000" : original.Color.Value; // Red for index 0

                // Add the stop back with the (potentially) new color
                stops.Add(
                    new DoubleValue(position, MeasureConst.NUM),
                    new ColorValue(colorHex, MeasureConst.Undefined)
                );
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
