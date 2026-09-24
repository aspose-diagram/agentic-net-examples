using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (X1,Y1, X2,Y2, X3,Y3) and close the shape by repeating the first point
            double[] trianglePoints = new double[]
            {
                4.0, 4.0,   // Vertex 1
                6.0, 4.0,   // Vertex 2
                5.0, 6.0,   // Vertex 3
                4.0, 4.0    // Close back to Vertex 1
            };

            // Draw the triangle using a polyline; this returns the shape ID
            long triangleShapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the returned ID
            Shape triangleShape = page.Shapes.GetShape(triangleShapeId);

            // Optional: set fill color to light blue
            triangleShape.Fill.FillForegnd.Value = "#ADD8E6";

            // Optional: set line color to dark blue and line weight
            triangleShape.Line.LineColor.Value = "#00008B";
            triangleShape.Line.LineWeight.Value = 0.02; // inches

            // Save the diagram to a VSDX file
            diagram.Save("TriangleShape.vsdx", SaveFileFormat.Vsdx);
        }
    }