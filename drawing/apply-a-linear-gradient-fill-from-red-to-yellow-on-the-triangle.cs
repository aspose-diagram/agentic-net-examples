using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Define the vertices of the triangle (x1,y1, x2,y2, x3,y3, back to x1,y1)
        double[] trianglePoints = new double[] { 2, 2, 5, 2, 3.5, 5, 2, 2 };

        // Draw the triangle shape; returns the shape ID as a long
        long shapeId = page.DrawPolyline(trianglePoints);

        // Retrieve the shape object using the ID (cast to int as required by GetShape)
        Shape triangle = page.Shapes.GetShape((int)shapeId);

        // Apply a linear gradient fill from red to yellow
        triangle.Fill.FillPattern.Value = 25; // Gradient fill pattern
        triangle.Fill.GradientFill.GradientEnabled.Value = BOOL.True; // Enable gradient
        triangle.Fill.GradientFill.GradientDir.Value = 0; // Left‑to‑right direction
        triangle.Fill.GradientFill.GradientStops.Clear(); // Remove any existing stops

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
