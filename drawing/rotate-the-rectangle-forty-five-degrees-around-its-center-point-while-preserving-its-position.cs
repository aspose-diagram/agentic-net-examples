using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram (contains a default page)
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Define rectangle parameters (center at (5,5), size 2x1 inches)
        double pinX = 5.0;   // X coordinate of the rectangle's center
        double pinY = 5.0;   // Y coordinate of the rectangle's center
        double width = 2.0;  // Width in inches
        double height = 1.0; // Height in inches

        // Draw the rectangle on the page; returns the shape ID
        long rectId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object using the returned ID
        Shape rectangle = page.Shapes.GetShape(rectId);

        // Rotate the rectangle 45 degrees around its center
        rectangle.SetAngle(45); // Angle is in degrees as per Aspose.Diagram API

        // Save the diagram to a PNG file to verify the result
        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
        diagram.Save("RotatedRectangle.png", saveOptions);
    }
}
