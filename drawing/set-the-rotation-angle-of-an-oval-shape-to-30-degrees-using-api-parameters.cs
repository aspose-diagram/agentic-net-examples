using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (uses the provided create rule)
        Diagram diagram = new Diagram();

        // Access the first page
        Page page = diagram.Pages[0];

        // Draw an oval (ellipse) on the page
        // Parameters: pinX, pinY, width, height
        long shapeId = page.DrawEllipse(5.0, 5.0, 2.0, 1.0);

        // Retrieve the created shape
        Shape oval = page.Shapes.GetShape(shapeId);

        // Convert 30 degrees to radians (Aspose.Diagram expects radians)
        double angleRadians = Math.PI / 6.0; // 30 degrees

        // Set the rotation angle of the oval
        oval.SetAngle(angleRadians);

        // Save the diagram (uses the provided save rule)
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
