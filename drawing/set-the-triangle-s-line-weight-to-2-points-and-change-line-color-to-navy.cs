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

        // Draw a triangle using a closed polyline.
        // Coordinates are in inches: (PinX, PinY) pairs.
        // The last point repeats the first to close the shape.
        double[] trianglePoints = new double[] { 2, 2, 4, 2, 3, 4, 2, 2 };
        long shapeId = page.DrawPolyline(trianglePoints);

        // Retrieve the shape object (GetShape expects an int)
        Shape triangle = page.Shapes.GetShape((int)shapeId);

        // Set line weight to 2 points (2/72 inches)
        triangle.Line.LineWeight.Value = 2.0 / 72.0;

        // Set line color to navy using a hex string
        triangle.Line.LineColor.Value = "#000080";

        // Save the diagram to a VSDX file
        diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
