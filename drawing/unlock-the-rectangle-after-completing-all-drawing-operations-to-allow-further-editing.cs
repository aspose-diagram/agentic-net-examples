using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Get the first page (default page)
        Page page = diagram.Pages[0];

        // Draw a rectangle: (pinX, pinY, width, height)
        double pinX = 2.0;
        double pinY = 2.0;
        double width = 3.0;
        double height = 2.0;
        long rectId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the rectangle shape
        Shape rectShape = page.Shapes.GetShape(rectId);
        if (rectShape == null)
        {
            throw new Exception("Failed to retrieve the rectangle shape.");
        }

        // Unlock the rectangle by clearing protection locks
        rectShape.Protection.LockMoveX.Value = BOOL.False;
        rectShape.Protection.LockMoveY.Value = BOOL.False;
        rectShape.Protection.LockWidth.Value = BOOL.False;
        rectShape.Protection.LockHeight.Value = BOOL.False;
        rectShape.Protection.LockRotate.Value = BOOL.False;
        rectShape.Protection.LockVtxEdit.Value = BOOL.False;

        // Save the diagram to a VSDX file
        diagram.Save("UnlockedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
