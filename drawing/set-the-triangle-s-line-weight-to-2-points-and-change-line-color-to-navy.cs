using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Define the triangle vertices (x1,y1, x2,y2, x3,y3, back to x1,y1 to close)
        double[] trianglePoints = new double[] { 2, 2, 5, 2, 3.5, 5, 2, 2 };

        // Draw the triangle; returns the shape ID (long)
        long shapeId = page.DrawPolyline(trianglePoints);

        // Retrieve the shape object using the ID
        Shape triangle = page.Shapes.GetShape((int)shapeId);

        // Set line weight to 2 points (2/72 inches)
        triangle.Line.LineWeight.Value = 2.0 / 72.0;

        // Set line color to navy (hex code)
        triangle.Line.LineColor.Value = "#000080";

        // Save the diagram to a VSDX file
        diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
