using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (always exists in a new diagram)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
            long rectId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape(rectId);

            // Lock the rectangle to prevent modifications during batch processing
            rectShape.Protection.LockMoveX.Value = BOOL.True;
            rectShape.Protection.LockMoveY.Value = BOOL.True;
            rectShape.Protection.LockWidth.Value = BOOL.True;
            rectShape.Protection.LockHeight.Value = BOOL.True;
            rectShape.Protection.LockRotate.Value = BOOL.True;
            rectShape.Protection.LockVtxEdit.Value = BOOL.True;

            // (Optional) Save the diagram to verify the shape is locked
            diagram.Save("LockedRectangle.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
