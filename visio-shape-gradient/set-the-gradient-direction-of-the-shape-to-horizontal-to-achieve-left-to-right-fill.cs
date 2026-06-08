using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name
            long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle");

            // Retrieve the shape instance
            Shape shape = page.Shapes.GetShape(shapeId);

            // Enable gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to horizontal (left‑to‑right)
            shape.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

            // Define gradient stops (red to green)
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
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
