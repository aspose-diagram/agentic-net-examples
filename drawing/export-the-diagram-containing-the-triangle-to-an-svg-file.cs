using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Draw a triangle using a polyline.
        // The points are: (2,2) -> (4,2) -> (3,4) -> back to (2,2) to close the shape.
        long triangleId = page.DrawPolyline(new double[] { 2, 2, 4, 2, 3, 4, 2, 2 });

        // Optionally retrieve the shape if further modifications are needed
        // Shape triangle = page.Shapes.GetShape(triangleId);

        // Export the diagram (including the triangle) to SVG format
        SVGSaveOptions svgOptions = new SVGSaveOptions();
        diagram.Save("triangle.svg", svgOptions);
    }
}
