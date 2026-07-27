using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Create a new diagram (or load an existing one)
        // Using the lifecycle rule for creation
        Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram();

        // Get the first page where shapes will be drawn
        Aspose.Diagram.Page page = diagram.Pages[0];

        // Parameters for the triangle grid
        int rows = 2;
        int cols = 5;
        double triangleWidth = 1.0;   // width of each triangle
        double triangleHeight = 1.0;  // height of each triangle
        double startX = 1.0;          // left margin
        double startY = 1.0;          // top margin
        double spacingX = 2.0;        // horizontal distance between triangle origins
        double spacingY = 2.0;        // vertical distance between triangle origins

        // Define the points of a single triangle relative to its pin (0,0)
        // The points are: left-bottom, right-bottom, top-center, back to left-bottom to close
        double[] trianglePoints = new double[]
        {
            0, triangleHeight,                     // left-bottom
            triangleWidth, triangleHeight,         // right-bottom
            triangleWidth / 2, 0,                  // top-center
            0, triangleHeight                      // back to left-bottom (optional closing point)
        };

        // Loop through rows and columns to place ten identical triangles
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Calculate the pin (origin) for the current triangle
                double pinX = startX + col * spacingX;
                double pinY = startY + row * spacingY;

                // Draw the triangle as a polyline on the page
                // Using the DrawPolyline method (lifecycle rule)
                page.DrawPolyline(pinX, pinY, triangleWidth, triangleHeight, trianglePoints);
            }
        }

        // Save the diagram using the provided save rule
        diagram.Save("TrianglesGrid.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);
    }
}
