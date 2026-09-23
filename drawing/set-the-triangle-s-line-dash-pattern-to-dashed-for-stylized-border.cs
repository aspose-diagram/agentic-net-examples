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

        // Define triangle vertices (X1,Y1, X2,Y2, X3,Y3, X1,Y1 to close)
        double[] trianglePoints = new double[] { 2.0, 2.0, 4.0, 2.0, 3.0, 4.0, 2.0, 2.0 };

        // Draw the triangle; returns the shape ID (long)
        long triangleId = page.DrawPolyline(trianglePoints);

        // Retrieve the shape object using the ID
        Shape triangle = page.Shapes.GetShape(triangleId);

        // Set the line dash pattern to dashed
        triangle.Line.LinePattern.Value = LinePatternValue.Dash;

        // Save the diagram to a VSDX file
        diagram.Save("TriangleDashed.vsdx", SaveFileFormat.Vsdx);
    }
}
