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

            // Define points for a triangle (closed polygon)
            // Points are specified as a flat double array: x1, y1, x2, y2, ...
            double[] trianglePoints = new double[]
            {
                0, 0,   // Point 1
                2, 0,   // Point 2
                1, 2,   // Point 3
                0, 0    // Close the shape by returning to Point 1
            };

            // Draw the triangle on the page; returns the shape ID (long)
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID
            Shape triangle = page.Shapes.GetShape((int)triangleId);

            // Set the line dash pattern to dashed
            triangle.Line.LinePattern.Value = LinePatternValue.Dash;

            // Optional: save the diagram to verify the result
            diagram.Save("Triangle.vsdx", SaveFileFormat.Vsdx);
        }
    }