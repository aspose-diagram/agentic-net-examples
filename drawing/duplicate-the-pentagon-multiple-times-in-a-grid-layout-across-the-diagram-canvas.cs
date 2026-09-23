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

        // Base pentagon points (relative to (0,0))
        // The points define a simple pentagon shape
        double[] basePoints = new double[]
        {
            0, 0,      // Point 1
            2, 0,      // Point 2
            2.5, 2,    // Point 3
            1, 3,      // Point 4
            -0.5, 2    // Point 5
            // Closing point will be added later
        };

        // Grid configuration
        int rows = 5;               // number of rows
        int cols = 6;               // number of columns
        double startX = 1.0;        // starting X coordinate
        double startY = 1.0;        // starting Y coordinate
        double hSpacing = 3.0;      // horizontal spacing between pentagons
        double vSpacing = 4.0;      // vertical spacing between pentagons

        // Loop to create a grid of pentagons
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                double offsetX = startX + col * hSpacing;
                double offsetY = startY + row * vSpacing;

                // Build the point array for this copy, applying the offset
                double[] points = new double[basePoints.Length + 2]; // extra two for closing point
                for (int i = 0; i < basePoints.Length; i += 2)
                {
                    points[i] = basePoints[i] + offsetX;       // X coordinate + offset
                    points[i + 1] = basePoints[i + 1] + offsetY; // Y coordinate + offset
                }
                // Add closing point (same as first point) to close the polygon
                points[points.Length - 2] = basePoints[0] + offsetX;
                points[points.Length - 1] = basePoints[1] + offsetY;

                // Draw the pentagon on the page
                long shapeId = page.DrawPolyline(points);

                // Optional: retrieve the shape if further modifications are needed
                // Shape pentagon = page.Shapes.GetShape(shapeId);
            }
        }

        // Save the diagram to a VSDX file
        diagram.Save("PentagonGrid.vsdx", SaveFileFormat.Vsdx);
    }
}
