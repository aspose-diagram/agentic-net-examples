using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Clear protection on all shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    ClearShapeProtection(shape);
                }
            }

            // Validate that no protection flags remain set
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    ValidateShapeProtectionCleared(shape);
                }
            }

            // Optionally save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Protection cleared and validation completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Sets all lock properties of a shape to FALSE
    private static void ClearShapeProtection(Shape shape)
    {
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
    }

    // Throws an exception if any lock property is still TRUE
    private static void ValidateShapeProtectionCleared(Shape shape)
    {
        if (shape.Protection.LockAspect.Value == BOOL.True) ThrowLockError(shape, "LockAspect");
        if (shape.Protection.LockBegin.Value == BOOL.True) ThrowLockError(shape, "LockBegin");
        if (shape.Protection.LockCalcWH.Value == BOOL.True) ThrowLockError(shape, "LockCalcWH");
        if (shape.Protection.LockCrop.Value == BOOL.True) ThrowLockError(shape, "LockCrop");
        if (shape.Protection.LockCustProp.Value == BOOL.True) ThrowLockError(shape, "LockCustProp");
        if (shape.Protection.LockDelete.Value == BOOL.True) ThrowLockError(shape, "LockDelete");
        if (shape.Protection.LockEnd.Value == BOOL.True) ThrowLockError(shape, "LockEnd");
        if (shape.Protection.LockFormat.Value == BOOL.True) ThrowLockError(shape, "LockFormat");
        if (shape.Protection.LockFromGroupFormat.Value == BOOL.True) ThrowLockError(shape, "LockFromGroupFormat");
        if (shape.Protection.LockGroup.Value == BOOL.True) ThrowLockError(shape, "LockGroup");
        if (shape.Protection.LockHeight.Value == BOOL.True) ThrowLockError(shape, "LockHeight");
        if (shape.Protection.LockMoveX.Value == BOOL.True) ThrowLockError(shape, "LockMoveX");
        if (shape.Protection.LockMoveY.Value == BOOL.True) ThrowLockError(shape, "LockMoveY");
        if (shape.Protection.LockRotate.Value == BOOL.True) ThrowLockError(shape, "LockRotate");
        if (shape.Protection.LockSelect.Value == BOOL.True) ThrowLockError(shape, "LockSelect");
        if (shape.Protection.LockTextEdit.Value == BOOL.True) ThrowLockError(shape, "LockTextEdit");
        if (shape.Protection.LockThemeColors.Value == BOOL.True) ThrowLockError(shape, "LockThemeColors");
        if (shape.Protection.LockThemeEffects.Value == BOOL.True) ThrowLockError(shape, "LockThemeEffects");
        if (shape.Protection.LockVtxEdit.Value == BOOL.True) ThrowLockError(shape, "LockVtxEdit");
        if (shape.Protection.LockWidth.Value == BOOL.True) ThrowLockError(shape, "LockWidth");
    }

    private static void ThrowLockError(Shape shape, string lockName)
    {
        string message = $"Shape ID {shape.ID} still has protection lock '{lockName}' set to TRUE.";
        Console.WriteLine(message);
        throw new Exception(message);
    }
}
