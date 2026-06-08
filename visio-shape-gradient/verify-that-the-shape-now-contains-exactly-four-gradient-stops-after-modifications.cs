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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape to modify (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25;                     // Set fill pattern to gradient
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Clear any existing gradient stops
            shape.Fill.GradientFill.GradientStops.Clear();

            // Add exactly four gradient stops
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.0, MeasureConst.NUM), new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.33, MeasureConst.NUM), new ColorValue("#00FF00", MeasureConst.Undefined)); // Green
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0.66, MeasureConst.NUM), new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1.0, MeasureConst.NUM), new ColorValue("#FFFF00", MeasureConst.Undefined)); // Yellow at end

            // Verify that the shape now contains exactly four gradient stops
            int stopCount = shape.Fill.GradientFill.GradientStops.Count;
            if (stopCount != 4)
            {
                throw new Exception($"Gradient stop verification failed. Expected 4 stops, but found {stopCount}.");
            }
            else
            {
                Console.WriteLine("Verification succeeded: shape contains exactly four gradient stops.");
            }

            // (Optional) Save the diagram to persist changes
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
