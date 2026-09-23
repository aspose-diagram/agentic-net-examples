using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Draw a rectangle shape (returns the shape ID)
        long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

        // Retrieve the shape object using the ID
        Shape shape = page.Shapes.GetShape(shapeId);

        // Enable gradient fill
        shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

        // Set gradient direction to vertical (top‑to‑bottom)
        shape.Fill.GradientFill.GradientDir.Value = 1; // 1 = vertical

        // Optional: define gradient stops (top red, bottom green)
        shape.Fill.GradientFill.GradientStops.Clear();
        shape.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(0, MeasureConst.NUM),
            new ColorValue("#FF0000", MeasureConst.Undefined));
        shape.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(1, MeasureConst.NUM),
            new ColorValue("#00FF00", MeasureConst.Undefined));

        // Save the diagram to a VSDX file
        diagram.Save("GradientVertical.vsdx", SaveFileFormat.Vsdx);
    }
}
