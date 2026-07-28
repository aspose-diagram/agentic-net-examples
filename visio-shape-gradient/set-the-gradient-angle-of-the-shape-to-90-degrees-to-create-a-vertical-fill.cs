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

            // Load an existing Visio diagram (or create a new one)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: PinX, PinY, Width, Height, Master name
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle");

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set the gradient angle to 90 degrees (vertical fill)
            shape.Fill.GradientFill.GradientAngle.Value = 90;

            // Optional: define gradient direction (0 = left‑to‑right, 1 = top‑to‑bottom, etc.)
            // Here we use 0 for left‑to‑right; the angle already forces vertical orientation
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stops (position 0 = start color, position 1 = end color)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start

            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at end

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
