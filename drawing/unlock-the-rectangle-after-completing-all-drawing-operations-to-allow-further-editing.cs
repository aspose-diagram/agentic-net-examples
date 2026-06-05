using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Ensure there is at least one page to work with
        Page page;
        if (diagram.Pages.Count == 0)
        {
            page = new Page();
            diagram.Pages.Add(page);
        }
        else
        {
            page = diagram.Pages[0];
        }

        // Draw a rectangle (pinX, pinY, width, height)
        long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

        // Retrieve the shape object using the returned ID
        Shape rectShape = page.Shapes.GetShape((int)rectId);

        // Unlock the rectangle to allow further editing
        rectShape.Protection.LockMoveX.Value = BOOL.False;
        rectShape.Protection.LockMoveY.Value = BOOL.False;
        rectShape.Protection.LockWidth.Value = BOOL.False;
        rectShape.Protection.LockHeight.Value = BOOL.False;
        rectShape.Protection.LockRotate.Value = BOOL.False;
        rectShape.Protection.LockVtxEdit.Value = BOOL.False;
        rectShape.Protection.LockAspect.Value = BOOL.False;

        // Save the diagram to a VSDX file
        diagram.Save("UnlockedRectangle.vsdx", SaveFileFormat.Vsdx);
    }
}
