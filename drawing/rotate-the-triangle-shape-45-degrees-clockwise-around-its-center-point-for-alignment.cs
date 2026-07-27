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

            // Define the three vertices of the triangle (in inches)
            // Example coordinates form an equilateral triangle
            double x1 = 2.0, y1 = 2.0;
            double x2 = 4.0, y2 = 2.0;
            double x3 = 3.0, y3 = 4.0;

            // Draw the triangle using a polyline.
            // The first point is repeated at the end to close the shape.
            page.DrawPolyline(new double[]
            {
                x1, y1,
                x2, y2,
                x3, y3,
                x1, y1   // close the polygon
            });

            // Retrieve the shape that was just added.
            // The newly drawn shape will be the last one in the collection.
            Shape triangle = page.Shapes[page.Shapes.Count - 1];

            // Rotate the triangle 45 degrees clockwise around its center.
            // SetAngle expects the rotation angle in degrees (per Aspose.Diagram rule set).
            triangle.SetAngle(45);

            // Save the diagram to a VSDX file.
            diagram.Save("TriangleRotated.vsdx", SaveFileFormat.Vsdx);
        }
    }