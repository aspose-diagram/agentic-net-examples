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

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1); // shape ID 1 (adjust as needed)

            // Configure the shape to use a gradient fill
            // Set fill pattern to Gradient (value 25)
            shape.Fill.FillPattern.Value = 25;

            // Enable the gradient
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction (0 = horizontal, 1 = vertical, etc.)
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add new gradient stops (position 0.0 = start, 1.0 = end)
            // Start color: blue
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // End color: green
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
