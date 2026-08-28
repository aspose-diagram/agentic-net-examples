using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page where we will draw the rectangle
            Page page = diagram.ActivePage;

            // Draw a rectangle at position (2,2) with width 4 and height 2 inches
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Set the fill pattern to gradient (value 25)
            rectShape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            rectShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to linear
            rectShape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Clear any existing gradient stops
            rectShape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stop at position 0 (blue)
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));

            // Add gradient stop at position 1 (green)
            rectShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientRectangle.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
