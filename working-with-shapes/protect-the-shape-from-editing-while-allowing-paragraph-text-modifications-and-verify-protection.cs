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

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");
            Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

            // Apply protection: lock most editing actions, but allow text editing and formatting
            shape.Protection.LockMoveX.Value = BOOL.True;
            shape.Protection.LockMoveY.Value = BOOL.True;
            shape.Protection.LockWidth.Value = BOOL.True;
            shape.Protection.LockHeight.Value = BOOL.True;
            shape.Protection.LockRotate.Value = BOOL.True;
            shape.Protection.LockDelete.Value = BOOL.True;
            shape.Protection.LockGroup.Value = BOOL.True;
            shape.Protection.LockAspect.Value = BOOL.True;
            shape.Protection.LockBegin.Value = BOOL.True;
            shape.Protection.LockEnd.Value = BOOL.True;
            shape.Protection.LockCalcWH.Value = BOOL.True;
            shape.Protection.LockCrop.Value = BOOL.True;
            shape.Protection.LockCustProp.Value = BOOL.True;
            shape.Protection.LockFormat.Value = BOOL.False;      // allow formatting (paragraph changes)
            shape.Protection.LockTextEdit.Value = BOOL.False;   // allow text editing
            shape.Protection.LockSelect.Value = BOOL.True;
            shape.Protection.LockThemeColors.Value = BOOL.True;
            shape.Protection.LockThemeEffects.Value = BOOL.True;
            shape.Protection.LockVtxEdit.Value = BOOL.True;

            // Verify protection settings
            Verify(shape.Protection.LockMoveX.Value == BOOL.True, "LockMoveX");
            Verify(shape.Protection.LockMoveY.Value == BOOL.True, "LockMoveY");
            Verify(shape.Protection.LockWidth.Value == BOOL.True, "LockWidth");
            Verify(shape.Protection.LockHeight.Value == BOOL.True, "LockHeight");
            Verify(shape.Protection.LockRotate.Value == BOOL.True, "LockRotate");
            Verify(shape.Protection.LockDelete.Value == BOOL.True, "LockDelete");
            Verify(shape.Protection.LockGroup.Value == BOOL.True, "LockGroup");
            Verify(shape.Protection.LockAspect.Value == BOOL.True, "LockAspect");
            Verify(shape.Protection.LockBegin.Value == BOOL.True, "LockBegin");
            Verify(shape.Protection.LockEnd.Value == BOOL.True, "LockEnd");
            Verify(shape.Protection.LockCalcWH.Value == BOOL.True, "LockCalcWH");
            Verify(shape.Protection.LockCrop.Value == BOOL.True, "LockCrop");
            Verify(shape.Protection.LockCustProp.Value == BOOL.True, "LockCustProp");
            Verify(shape.Protection.LockFormat.Value == BOOL.False, "LockFormat (should be false)");
            Verify(shape.Protection.LockTextEdit.Value == BOOL.False, "LockTextEdit (should be false)");
            Verify(shape.Protection.LockSelect.Value == BOOL.True, "LockSelect");
            Verify(shape.Protection.LockThemeColors.Value == BOOL.True, "LockThemeColors");
            Verify(shape.Protection.LockThemeEffects.Value == BOOL.True, "LockThemeEffects");
            Verify(shape.Protection.LockVtxEdit.Value == BOOL.True, "LockVtxEdit");

            // Save the diagram to a file
            diagram.Save("ProtectedShape.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully with protection applied.");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }

    static void Verify(bool condition, string propertyName)
    {
        if (!condition)
        {
            throw new Exception($"Verification failed for protection property: {propertyName}");
        }
        else
        {
            Console.WriteLine($"Verified: {propertyName}");
        }
    }
}
