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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example ID = 1)
            // Adjust the ID as needed for your specific diagram
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 1 not found.");
                return;
            }

            // Enable gradient fill and set a vertical gradient (90 degrees)
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientAngle.Value = 90; // 90° creates a vertical fill

            // Optionally, define gradient stops (example: blue to green)
            shape.Fill.GradientFill.GradientStops.Clear();
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with vertical gradient fill.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
