using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Identify the target shape (example: shape with ID = 5)
            // You can also locate by name using shape.NameU or other criteria.
            long targetShapeId = 5;
            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(targetShapeId);

            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Remove protection by setting all lock properties to FALSE
            shape.Protection.LockAspect.Value = BOOL.False;
            shape.Protection.LockBegin.Value = BOOL.False;
            shape.Protection.LockCalcWH.Value = BOOL.False;
            shape.Protection.LockCrop.Value = BOOL.False;
            shape.Protection.LockCustProp.Value = BOOL.False;
            shape.Protection.LockDelete.Value = BOOL.False;
            shape.Protection.LockEnd.Value = BOOL.False;
            shape.Protection.LockFormat.Value = BOOL.False;
            shape.Protection.LockFromGroupFormat.Value = BOOL.False;
            shape.Protection.LockGroup.Value = BOOL.False;
            shape.Protection.LockHeight.Value = BOOL.False;
            shape.Protection.LockMoveX.Value = BOOL.False;
            shape.Protection.LockMoveY.Value = BOOL.False;
            shape.Protection.LockRotate.Value = BOOL.False;
            shape.Protection.LockSelect.Value = BOOL.False;
            shape.Protection.LockTextEdit.Value = BOOL.False;
            shape.Protection.LockThemeColors.Value = BOOL.False;
            shape.Protection.LockThemeEffects.Value = BOOL.False;
            shape.Protection.LockVtxEdit.Value = BOOL.False;
            shape.Protection.LockWidth.Value = BOOL.False;

            // Optionally, also clear the deletion flag if it was set
            shape.Del = BOOL.False;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Protection removed and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
