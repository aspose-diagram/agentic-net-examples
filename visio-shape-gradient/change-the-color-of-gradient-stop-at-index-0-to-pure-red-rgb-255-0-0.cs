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

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses shape ID 1)
            // Adjust the shape ID as needed for your diagram
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape uses a gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Clear existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add a new gradient stop at index 0 with pure red color (#FF0000)
            // Position 0 (MeasureConst.NUM) represents the start of the gradient
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            // (Optional) Add additional stops if needed, e.g., at position 1
            // shape.Fill.GradientFill.GradientStops.Add(
            //     new DoubleValue(1, MeasureConst.NUM),
            //     new ColorValue("#00FF00", MeasureConst.Undefined));

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
