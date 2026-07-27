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

        // Use the default first page
        Page page = diagram.Pages[0];

        // Draw a rectangle (PinX, PinY, Width, Height) in inches
        long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

        // Retrieve the shape object from the page
        Shape rectShape = page.Shapes.GetShape((int)rectId);

        // Unlock all protection flags so the rectangle can be edited later
        rectShape.Protection.LockMoveX.Value = BOOL.False;
        rectShape.Protection.LockMoveY.Value = BOOL.False;
        rectShape.Protection.LockWidth.Value = BOOL.False;
        rectShape.Protection.LockHeight.Value = BOOL.False;
        rectShape.Protection.LockRotate.Value = BOOL.False;
        rectShape.Protection.LockVtxEdit.Value = BOOL.False;

        // Ensure the shape is not marked as deleted
        if (rectShape.Del == BOOL.True)
        {
            rectShape.Del = BOOL.False;
        }

        // Save the diagram to a VSDX file
        diagram.Save("UnlockedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
