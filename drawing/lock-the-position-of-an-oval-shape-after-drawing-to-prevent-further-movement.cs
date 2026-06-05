using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Draw an oval (ellipse) at PinX=5, PinY=5 with Width=2, Height=1
        long shapeId = page.DrawEllipse(5.0, 5.0, 2.0, 1.0);

        // Retrieve the shape instance using the returned ID
        Shape oval = page.Shapes.GetShape(shapeId);

        // Lock the shape's position to prevent movement
        oval.Protection.LockMoveX.Value = BOOL.True;
        oval.Protection.LockMoveY.Value = BOOL.True;

        // Optionally lock other transformations (width, height, rotation) for full protection
        oval.Protection.LockWidth.Value = BOOL.True;
        oval.Protection.LockHeight.Value = BOOL.True;
        oval.Protection.LockRotate.Value = BOOL.True;

        // Save the diagram to a VSDX file
        diagram.Save("LockedOval.vsdx", SaveFileFormat.Vsdx);
    }
}
