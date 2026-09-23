using System.IO;
using System;
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

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 1 not found.");
                return;
            }

            // Enable gradient fill and set it to horizontal (left‑to‑right)
            shape.Fill.FillPattern.Value = 25;                         // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Turn on gradient
            shape.Fill.GradientFill.GradientDir.Value = 0;             // 0 = horizontal direction

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add two gradient stops: start (red) and end (green)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),                 // Position at start (0%)
                new ColorValue("#FF0000", MeasureConst.Undefined));   // Red color

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),                 // Position at end (100%)
                new ColorValue("#00FF00", MeasureConst.Undefined));   // Green color

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with horizontal gradient fill.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
