using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty Visio diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Triangle geometry (base points and height)
            double leftX = 2.0;
            double rightX = 3.0;
            double topX = 2.5;
            double triangleHeight = 1.0; // vertical size of the triangle

            // We need the original triangle plus three duplicates (total 4)
            int totalTriangles = 4;
            double verticalSpacing = 3.0; // space between each triangle

            for (int i = 0; i < totalTriangles; i++)
            {
                // Compute the Y coordinate for the current triangle's base
                double baseY = 2.0 + i * verticalSpacing;

                // Define the four points of the closed polyline (triangle)
                double[] points = new double[]
                {
                    leftX,  baseY,          // left base point
                    rightX, baseY,          // right base point
                    topX,   baseY + triangleHeight, // top point
                    leftX,  baseY           // close back to the first point
                };

                // Draw the triangle; DrawPolyline returns the shape ID (long)
                long shapeId = page.DrawPolyline(points);

                // Retrieve the shape to set visual properties
                Shape triangle = page.Shapes.GetShape((int)shapeId);
                triangle.Fill.FillForegnd.Value = "#FFCC00"; // light orange fill
                triangle.Line.LineColor.Value = "#000000";   // black outline
            }

            // Save the diagram as a VSDX file
            diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
