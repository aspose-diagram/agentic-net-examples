using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first shape on the first page (adjust the shape ID as needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Ensure the shape uses a gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Capture existing gradient stop positions and colors
            List<double> positions = new List<double>();
            List<string> colors = new List<string>();
            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
            {
                positions.Add(stop.Position.Value);
                colors.Add(stop.Color.Value);
            }

            // Change the color of the gradient stop at index 1 to pure green (#00FF00)
            if (colors.Count > 1)
            {
                colors[1] = "#00FF00";
            }

            // Rebuild the gradient stops collection with the updated colors
            shape.Fill.GradientFill.GradientStops.Clear();
            for (int i = 0; i < positions.Count; i++)
            {
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(positions[i], MeasureConst.NUM),
                    new ColorValue(colors[i], MeasureConst.Undefined));
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
