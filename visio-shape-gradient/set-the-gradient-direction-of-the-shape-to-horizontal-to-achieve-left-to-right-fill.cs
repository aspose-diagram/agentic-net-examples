using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page
            // Parameters: PinX, PinY, master name
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using the returned ID
            Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25;                     // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to horizontal (left‑to‑right)
            // 0 = left‑to‑right, 1 = top‑to‑bottom, etc.
            shape.Fill.GradientFill.GradientDir.Value = 0;

            // Define gradient stops (red at start, green at end)
            shape.Fill.GradientFill.GradientStops.Clear();
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));
            shape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the diagram to a VSDX file
            diagram.Save("GradientShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
