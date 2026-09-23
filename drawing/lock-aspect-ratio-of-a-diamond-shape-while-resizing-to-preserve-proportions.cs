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

        // Define points for a diamond shape (closed polyline)
        // Points: top, right, bottom, left, back to top
        double[] diamondPoints = new double[]
        {
            5.0, 6.0,   // Top
            6.0, 5.0,   // Right
            5.0, 4.0,   // Bottom
            4.0, 5.0,   // Left
            5.0, 6.0    // Close back to Top
        };

        // Draw the diamond shape; returns the shape ID (long)
        long diamondShapeId = page.DrawPolyline(diamondPoints);

        // Retrieve the shape object using the ID
        Shape diamondShape = page.Shapes.GetShape(diamondShapeId);

        // Lock the aspect ratio so that width and height stay proportional when resized
        diamondShape.Protection.LockAspect.Value = BOOL.True;

        // Example resize: change the width; height will follow the locked aspect ratio automatically
        diamondShape.XForm.Width.Value = 3.0; // New width (in inches)
        // Height is automatically adjusted by Visio when the diagram is edited in the UI.
        // Programmatically, you could also set Height to maintain the same ratio if needed.

        // Save the diagram to a VSDX file
        diagram.Save("DiamondLockedAspect.vsdx", SaveFileFormat.Vsdx);
    }
}
