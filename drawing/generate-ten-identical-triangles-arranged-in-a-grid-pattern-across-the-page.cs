using System;
using System.IO;
using Aspose.Diagram;
using System.Drawing;

class Program
{
    static void Main()
    {
        // Create a new diagram (uses the provided create rule)
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Triangle dimensions
        double triangleWidth = 1.0;   // width of each triangle
        double triangleHeight = 1.0;  // height of each triangle

        // Spacing between triangles
        double spacingX = 0.5;
        double spacingY = 0.5;

        // Starting position (pin point) for the grid
        double startX = 1.0;
        double startY = 1.0;

        // Create a 2 × 5 grid (total 10 triangles)
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                // Calculate the pin position for the current triangle
                double pinX = startX + col * (triangleWidth + spacingX);
                double pinY = startY + row * (triangleHeight + spacingY);

                // Define the three vertices of the triangle and close the shape
                double[] points = new double[]
                {
                    0, 0,                                 // Vertex 1 (bottom‑left)
                    triangleWidth, 0,                     // Vertex 2 (bottom‑right)
                    triangleWidth / 2, triangleHeight,   // Vertex 3 (top)
                    0, 0                                  // Close back to Vertex 1
                };

                // Draw the triangle as a polyline (uses the provided DrawPolyline rule)
                page.DrawPolyline(pinX, pinY, triangleWidth, triangleHeight, points);
            }
        }

        // Save the diagram to a file (uses the provided save rule)
        diagram.Save("Triangles.vsdx", SaveFileFormat.Vsdx);
    }
}
