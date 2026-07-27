using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw a rectangle shape (pinX, pinY, width, height); returns shape ID as long
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the shape object using the ID (cast to int as required by GetShape)
            Shape rectShape = page.Shapes.GetShape((int)rectId);

            // Set fill pattern to gradient (value 25)
            rectShape.Fill.FillPattern.Value = 25;

            // Enable gradient fill
            rectShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to linear (assign enum as its underlying int value)
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
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}