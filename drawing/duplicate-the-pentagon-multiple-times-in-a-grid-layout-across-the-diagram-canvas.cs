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

        // Define the points of a closed pentagon (relative to its pin)
        double[] pentagonPoints = new double[]
        {
            0, 0.5,
            0.475, 0.154,
            0.293, -0.404,
            -0.293, -0.404,
            -0.475, 0.154,
            0, 0.5 // close the shape
        };

        // Grid configuration
        int rows = 5;
        int cols = 5;
        double spacingX = 2.0; // horizontal spacing in inches
        double spacingY = 2.0; // vertical spacing in inches
        double startX = 2.0;   // initial X offset
        double startY = 2.0;   // initial Y offset

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                double pinX = startX + c * spacingX;
                double pinY = startY + r * spacingY;

                // Draw the pentagon at the calculated position
                long shapeId = page.DrawPolyline(pinX, pinY, 1.0, 1.0, pentagonPoints);

                // Retrieve the shape to apply styling (optional)
                Shape shape = page.Shapes.GetShape((int)shapeId);
                shape.Fill.FillForegnd.Value = "#ADD8E6"; // light blue fill
                shape.Line.LineColor.Value = "#0000FF";   // blue outline
            }
        }

        // Save the diagram in VSDX format
        diagram.Save("PentagonGrid.vsdx", SaveFileFormat.Vsdx);
    }
}
