using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page
            // Parameters: PinX, PinY, master name, isCalculate (bool)
            long rectId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape rect = diagram.ActivePage.Shapes.GetShape((int)rectId);

            // Lock the rectangle to prevent modifications during batch processing
            rect.Protection.LockMoveX.Value = BOOL.True;
            rect.Protection.LockMoveY.Value = BOOL.True;
            rect.Protection.LockWidth.Value = BOOL.True;
            rect.Protection.LockHeight.Value = BOOL.True;
            rect.Protection.LockRotate.Value = BOOL.True;
            rect.Protection.LockVtxEdit.Value = BOOL.True;

            // ------------------------------
            // Perform batch drawing operations here
            // (e.g., add other shapes, connectors, styling, etc.)
            // ------------------------------

            // After all drawing steps are completed, unlock the rectangle
            rect.Protection.LockMoveX.Value = BOOL.False;
            rect.Protection.LockMoveY.Value = BOOL.False;
            rect.Protection.LockWidth.Value = BOOL.False;
            rect.Protection.LockHeight.Value = BOOL.False;
            rect.Protection.LockRotate.Value = BOOL.False;
            rect.Protection.LockVtxEdit.Value = BOOL.False;

            // Save the diagram to a VSDX file
            diagram.Save("LockedRectangleDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
