using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the active page (first page) for drawing
            Page page = diagram.ActivePage;

            // Draw an oval (ellipse) with center at (5,5) and size 2x1 inches
            long ovalId = page.DrawEllipse(5.0, 5.0, 2.0, 1.0);

            // Retrieve the shape object representing the oval
            Shape oval = page.Shapes.GetShape(ovalId);

            // Lock the oval's position to prevent moving
            oval.Protection.LockMoveX.Value = BOOL.True;
            oval.Protection.LockMoveY.Value = BOOL.True;

            // Optionally lock size to keep width and height unchanged
            oval.Protection.LockWidth.Value = BOOL.True;
            oval.Protection.LockHeight.Value = BOOL.True;

            // Save the diagram to a VSDX file
            diagram.Save("LockedOval.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with the oval shape locked.");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
