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

            // Define triangle vertices (X1,Y1, X2,Y2, X3,Y3)
            double[] trianglePoints = new double[] { 2.0, 2.0, 4.0, 2.0, 3.0, 4.0 };

            // Draw the triangle as a polyline; returns the shape ID (long)
            long shapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID
            Shape triangle = page.Shapes.GetShape((int)shapeId);

            // Set line weight to 2 points (2/72 inches)
            triangle.Line.LineWeight.Value = 2.0 / 72.0;

            // Set line color to navy (#000080)
            triangle.Line.LineColor.Value = "#000080";

            // Save the diagram to a VSDX file
            diagram.Save("TriangleStyled.vsdx", SaveFileFormat.Vsdx);
        }
    }