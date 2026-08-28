using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first (and only) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (in inches)
            double x1 = 2.0, y1 = 2.0;
            double x2 = 5.0, y2 = 2.0;
            double x3 = 3.5, y3 = 5.0;

            // Draw the triangle as a closed polyline (repeat first point at the end)
            long shapeId = page.DrawPolyline(new double[]
            {
                x1, y1,
                x2, y2,
                x3, y3,
                x1, y1
            });

            // Retrieve the shape object (cast long ID to int as required)
            Shape triangle = page.Shapes.GetShape((int)shapeId);

            // Uniform scaling factor
            double scaleFactor = 1.5;

            // Scale width and height while keeping the center (PinX/PinY) unchanged
            triangle.XForm.Width.Value *= scaleFactor;
            triangle.XForm.Height.Value *= scaleFactor;

            // Save the diagram to a VSDX file
            diagram.Save("ScaledTriangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
