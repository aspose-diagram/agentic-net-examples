using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Use the first page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, width, height, master name, isCalculate
                long rectId = page.AddShape(2.0, 2.0, 3.0, 1.5, "Rectangle", false);

                // Retrieve the shape object using the returned ID
                Shape rectangle = page.Shapes.GetShape(rectId);

                // Lock the rectangle to prevent modifications during batch processing
                rectangle.Protection.LockMoveX.Value = BOOL.True;
                rectangle.Protection.LockMoveY.Value = BOOL.True;
                rectangle.Protection.LockWidth.Value = BOOL.True;
                rectangle.Protection.LockHeight.Value = BOOL.True;
                rectangle.Protection.LockRotate.Value = BOOL.True;
                rectangle.Protection.LockVtxEdit.Value = BOOL.True;

                // -------------------------------------------------
                // Perform batch drawing steps here
                // (e.g., add other shapes, connectors, apply styles, etc.)
                // -------------------------------------------------

                // After all drawing steps are completed, unlock the rectangle
                rectangle.Protection.LockMoveX.Value = BOOL.False;
                rectangle.Protection.LockMoveY.Value = BOOL.False;
                rectangle.Protection.LockWidth.Value = BOOL.False;
                rectangle.Protection.LockHeight.Value = BOOL.False;
                rectangle.Protection.LockRotate.Value = BOOL.False;
                rectangle.Protection.LockVtxEdit.Value = BOOL.False;

                // Save the diagram to a VSDX file
                diagram.Save("LockedRectangleDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }