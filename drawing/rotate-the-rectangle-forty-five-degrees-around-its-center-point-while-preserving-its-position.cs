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

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Define rectangle center position and size
        double pinX = 5.0;   // X coordinate of the rectangle center
        double pinY = 5.0;   // Y coordinate of the rectangle center
        double width = 2.0;  // Width of the rectangle
        double height = 1.0; // Height of the rectangle

        // Draw the rectangle on the page
        long rectId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object using its ID
        Shape rectShape = page.Shapes.GetShape((int)rectId);

        // Rotate the rectangle 45 degrees around its center
        rectShape.SetAngle(45);

        // Save the diagram to a VSDX file
        diagram.Save("RotatedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
