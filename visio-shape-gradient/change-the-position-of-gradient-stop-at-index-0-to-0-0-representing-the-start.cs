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

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape (example: shape with ID 1)
            // Adjust the ID as needed for your diagram
            Shape shape = page.Shapes.GetShape(1);

            // Ensure gradient fill is enabled
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Store existing gradient stops
            List<GradientStop> existingStops = new List<GradientStop>();
            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
            {
                existingStops.Add(stop);
            }

            // Clear current stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Re‑add stops, modifying the position of the first stop to 0.0 (start)
            for (int i = 0; i < existingStops.Count; i++)
            {
                GradientStop original = existingStops[i];
                DoubleValue position = original.Position;
                ColorValue color = original.Color;

                if (i == 0)
                {
                    // Set position to the start of the gradient (0.0)
                    position = new DoubleValue(0.0, MeasureConst.NUM);
                }

                shape.Fill.GradientFill.GradientStops.Add(position, color);
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
