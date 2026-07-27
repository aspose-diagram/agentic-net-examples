using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page (default page)
        Page page = diagram.Pages[0];

        // Define oval (ellipse) parameters: center at (5,5) inches, width 3 inches, height 2 inches
        double pinX = 5.0;
        double pinY = 5.0;
        double width = 3.0;
        double height = 2.0;

        // Draw the oval; DrawEllipse returns the shape ID (long)
        long ovalId = page.DrawEllipse(pinX, pinY, width, height);

        // Retrieve the Shape object using the returned ID
        Shape oval = page.Shapes.GetShape(ovalId);

        // Lock the shape's position to prevent any movement
        oval.Protection.LockMoveX.Value = BOOL.True;
        oval.Protection.LockMoveY.Value = BOOL.True;

        // Optionally lock size to keep width/height unchanged
        oval.Protection.LockWidth.Value = BOOL.True;
        oval.Protection.LockHeight.Value = BOOL.True;

        // Save the diagram to a VSDX file
        diagram.Save("LockedOval.vsdx", SaveFileFormat.Vsdx);
    }
}
