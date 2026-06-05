using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (lifecycle rule: create)
        Diagram diagram = new Diagram();

        // Get the first page of the diagram
        Page page = diagram.Pages[0];

        // Define triangle size and spacing
        double triangleWidth = 1.0;   // width of each triangle
        double triangleHeight = 1.0;  // height of each triangle
        double spacingX = 0.5;        // horizontal spacing between triangles
        double spacingY = 0.5;        // vertical spacing between triangles

        // Grid layout: 2 rows x 5 columns = 10 triangles
        int rows = 2;
        int cols = 5;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Calculate the top‑left corner of the current triangle
                double startX = col * (triangleWidth + spacingX);
                double startY = row * (triangleHeight + spacingY);

                // Pin (center) position for the shape
                double pinX = startX + triangleWidth / 2.0;
                double pinY = startY + triangleHeight / 2.0;

                // Define the three points of the triangle (closed polyline)
                // Points are relative to the pin position
                double[] xyArray = new double[]
                {
                    -triangleWidth / 2.0, -triangleHeight / 2.0,   // bottom‑left
                     triangleWidth / 2.0, -triangleHeight / 2.0,   // bottom‑right
                     0.0,                triangleHeight / 2.0,    // top
                    -triangleWidth / 2.0, -triangleHeight / 2.0    // close back to start
                };

                // Draw the triangle using DrawPolyline (method rule)
                page.DrawPolyline(pinX, pinY, triangleWidth, triangleHeight, xyArray);
            }
        }

        // Save the diagram to a file (lifecycle rule: save)
        diagram.Save("TrianglesGrid.vsdx", SaveFileFormat.Vsdx);
    }
}
