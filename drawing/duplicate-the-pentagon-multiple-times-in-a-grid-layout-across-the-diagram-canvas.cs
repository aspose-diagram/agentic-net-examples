using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Define the base pentagon points (closed polygon)
        // Coordinates are in inches
        double[] basePoints = new double[]
        {
            1.0, 0.0,   // Point 1
            1.5, 1.0,   // Point 2
            0.5, 1.5,   // Point 3
            -0.5, 1.0,  // Point 4
            -1.0, 0.0,  // Point 5
            1.0, 0.0    // Close back to Point 1
        };

        // Approximate width and height of the pentagon for spacing calculations
        double shapeWidth = 2.5;   // max X - min X
        double shapeHeight = 1.5;  // max Y - min Y

        // Grid configuration
        int rows = 3;
        int cols = 4;
        double hSpacing = 0.5; // horizontal spacing between shapes
        double vSpacing = 0.5; // vertical spacing between shapes

        // Duplicate the pentagon across the grid
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                double offsetX = col * (shapeWidth + hSpacing);
                double offsetY = row * (shapeHeight + vSpacing);

                double[] shiftedPoints = new double[basePoints.Length];
                for (int i = 0; i < basePoints.Length; i += 2)
                {
                    shiftedPoints[i] = basePoints[i] + offsetX;       // X coordinate
                    shiftedPoints[i + 1] = basePoints[i + 1] + offsetY; // Y coordinate
                }

                // Draw the pentagon at the calculated position
                page.DrawPolyline(shiftedPoints);
            }
        }

        // Save the diagram to a VSDX file
        diagram.Save("PentagonGrid.vsdx", SaveFileFormat.Vsdx);
    }
}
