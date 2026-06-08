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

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape (using ID 1 as an example)
            Shape shape = page.Shapes.GetShape(1);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25;                     // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to vertical (top‑to‑bottom)
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear; // Linear gradient
            shape.Fill.GradientFill.GradientAngle.Value = 90;                       // 90° for vertical

            // Define gradient stops (optional – here red to blue)
            shape.Fill.GradientFill.GradientStops.Clear();
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
