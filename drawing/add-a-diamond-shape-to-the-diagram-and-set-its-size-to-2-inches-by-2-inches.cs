using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page (automatically created)
        Page page = diagram.Pages[0];

        // Define the center point and half dimensions for a 2"x2" diamond
        double centerX = 2.0;
        double centerY = 2.0;
        double halfWidth = 1.0;   // 2 inches total width
        double halfHeight = 1.0;  // 2 inches total height

        // Points defining the diamond (top, right, bottom, left, back to top)
        double[] points = new double[]
        {
            centerX, centerY - halfHeight, // top
            centerX + halfWidth, centerY,  // right
            centerX, centerY + halfHeight, // bottom
            centerX - halfWidth, centerY,  // left
            centerX, centerY - halfHeight  // close the shape
        };

        // Draw the diamond shape; returns a long shape ID
        long shapeId = page.DrawPolyline(points);

        // Retrieve the shape object (GetShape expects an int)
        Shape diamond = page.Shapes.GetShape((int)shapeId);

        // Set the size to exactly 2 inches by 2 inches
        diamond.XForm.Width.Value = 2.0;
        diamond.XForm.Height.Value = 2.0;

        // Position the shape at the defined center
        diamond.XForm.PinX.Value = centerX;
        diamond.XForm.PinY.Value = centerY;

        // Save the diagram to a VSDX file
        diagram.Save("DiamondDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
