using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Load an existing Visio diagram or create a new one
        // Here we create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page (created by default)
        Page page = diagram.Pages[0];

        // Draw a line shape on the page
        // Parameters: pinX, pinY, width, height, xyArray (relative points)
        // This creates a simple diagonal line
        double pinX = 2.0;   // X coordinate of the shape's pin
        double pinY = 2.0;   // Y coordinate of the shape's pin
        double width = 4.0;  // Width of the line shape
        double height = 4.0; // Height of the line shape
        double[] points = { 0, 0, width, height }; // start (0,0) to end (width,height)

        long shapeId = page.DrawLine(pinX, pinY, width, height, points);

        // Retrieve the newly created shape by its ID
        Shape lineShape = page.Shapes.GetShape(shapeId);

        // Set the line color transparency to 50% (0.5)
        // LineColorTrans expects a DoubleValue; assign the Value property
        lineShape.Line.LineColorTrans.Value = 0.5;

        // Optionally set a visible line color (e.g., black) to see the effect
        lineShape.Line.LineColor.Value = "RGB(0,0,0)";

        // Save the diagram as PDF to observe the line transparency
        diagram.Save("LineTransparency.pdf", SaveFileFormat.Pdf);
    }
}
