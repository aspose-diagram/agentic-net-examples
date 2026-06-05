using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Define triangle vertices (in inches)
        double[] trianglePoints = new double[]
        {
            2.0, 2.0,   // Point 1
            5.0, 2.0,   // Point 2
            3.5, 5.0    // Point 3
        };

        // Draw the triangle using a polyline (returns the shape ID)
        long shapeId = page.DrawPolyline(trianglePoints);

        // Retrieve the shape object
        Shape triangle = page.Shapes.GetShape((int)shapeId);

        // Apply a linear gradient fill from red to yellow
        triangle.Fill.FillPattern.Value = 25; // Gradient fill pattern
        triangle.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
        triangle.Fill.GradientFill.GradientDir.Value = (int)GradientFillDir.Linear;

        // Clear any existing gradient stops
        triangle.Fill.GradientFill.GradientStops.Clear();

        // Add gradient stop at position 0 (red)
        triangle.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(0, MeasureConst.NUM),
            new ColorValue("#FF0000", MeasureConst.Undefined));

        // Add gradient stop at position 1 (yellow)
        triangle.Fill.GradientFill.GradientStops.Add(
            new DoubleValue(1, MeasureConst.NUM),
            new ColorValue("#FFFF00", MeasureConst.Undefined));

        // Save the diagram to a VSDX file
        diagram.Save("TriangleGradient.vsdx", SaveFileFormat.Vsdx);
    }
}
