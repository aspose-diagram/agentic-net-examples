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
            var diagram = new Diagram("input.vsdx");

            // Access the first page (index 0)
            var page = diagram.Pages[0];

            // Retrieve a shape by its ID (example ID = 1)
            var shape = page.Shapes.GetShape(1);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at position 0.25 with yellow color (RGB 255,255,0)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.25, MeasureConst.NUM),
                new ColorValue("#FFFF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
