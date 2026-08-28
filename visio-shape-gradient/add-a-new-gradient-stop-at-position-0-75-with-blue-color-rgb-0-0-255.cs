using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page
        Page page = diagram.Pages[0];

        // Add a rectangle shape to the page (pinX, pinY, width, height)
        long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);

        // Retrieve the shape object (Shapes.GetShape expects an int)
        Shape shape = page.Shapes.GetShape((int)shapeId);

        // Enable gradient fill
        shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
        shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear; // Linear gradient

        // Clear any existing gradient stops
        shape.Fill.GradientFill.GradientStops.Clear();

        // Add a new gradient stop at position 0.75 with blue color (RGB 0,0,255)
        shape.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(0.75, MeasureConst.NUM),
            new ColorValue("#0000FF", MeasureConst.Undefined));

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
