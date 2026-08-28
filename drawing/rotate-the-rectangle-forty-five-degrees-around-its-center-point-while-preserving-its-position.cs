using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (uses the create rule)
        Diagram diagram = new Diagram();

        // Access the default page
        Page page = diagram.Pages[0];

        // Rectangle parameters (pinX, pinY define the center of rotation)
        double pinX = 5.0;   // X‑coordinate of the rectangle center
        double pinY = 5.0;   // Y‑coordinate of the rectangle center
        double width = 4.0;  // Width of the rectangle
        double height = 2.0; // Height of the rectangle

        // Draw the rectangle and obtain its shape ID
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object by its ID
        Shape rectShape = page.Shapes.GetShape(shapeId);

        // Rotate 45 degrees around its center (angle in radians)
        double angleRadians = Math.PI / 4; // 45° = π/4 rad
        rectShape.SetAngle(angleRadians);

        // Save the diagram (uses the save rule)
        diagram.Save("RotatedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
