using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Define the three vertices of the triangle using absolute coordinates (in inches)
                // The first point is repeated at the end to close the shape
                double[] trianglePoints = new double[]
                {
                    1.0, 1.0,   // Vertex 1
                    3.0, 1.0,   // Vertex 2
                    2.0, 3.0,   // Vertex 3
                    1.0, 1.0    // Close the triangle
                };

                // Draw the triangle as a closed polyline
                long shapeId = page.DrawPolyline(trianglePoints);

                // Retrieve the created shape for optional styling
                Shape triangleShape = page.Shapes.GetShape(shapeId);

                // Example styling: blue outline with a thin line weight
                triangleShape.Line.LineColor.Value = "#0000FF";
                triangleShape.Line.LineWeight.Value = 0.02; // inches

                // Save the diagram to a VSDX file
                diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }