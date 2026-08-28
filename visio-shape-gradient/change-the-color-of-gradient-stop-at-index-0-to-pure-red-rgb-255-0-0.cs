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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Retrieve a shape (example: shape with ID 1 on the first page)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Enable gradient fill for the shape
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (e.g., left to right)

            // Remove any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a gradient stop at index 0 with pure red color (RGB 255,0,0)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // (Optional) Add another stop to complete the gradient (e.g., blue at position 1)
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

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
