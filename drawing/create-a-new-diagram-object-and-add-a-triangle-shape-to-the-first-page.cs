using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define the vertices of a triangle (X1,Y1, X2,Y2, X3,Y3) and close the shape by repeating the first point
            double[] trianglePoints = new double[] { 2.0, 2.0, 4.0, 2.0, 3.0, 4.0, 2.0, 2.0 };

            // Draw the triangle on the page; returns the shape ID (long)
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the returned ID
            Shape triangle = page.Shapes.GetShape(triangleId);

            // Set visual properties for the triangle
            triangle.Fill.FillForegnd.Value = "#FF0000";      // Red fill
            triangle.Line.LineColor.Value = "#000000";      // Black outline
            triangle.Line.LineWeight.Value = 0.02;          // Line thickness (in inches)

            // Save the diagram to a VSDX file
            diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }