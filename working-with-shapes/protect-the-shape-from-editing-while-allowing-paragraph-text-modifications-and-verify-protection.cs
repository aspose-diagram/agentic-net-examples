using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        Diagram diagram;
        try
        {
            // Load the existing Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Work with the first page of the diagram
        Page page = diagram.Pages[0];

        // Locate a target shape (first non‑connector with a name)
        Shape targetShape = null;
        foreach (Shape shape in page.Shapes)
        {
            if (!shape.OneD && !string.IsNullOrEmpty(shape.NameU))
            {
                targetShape = shape;
                break;
            }
        }

        if (targetShape == null)
        {
            Console.WriteLine("No suitable shape found on the page.");
            return;
        }

        // Apply protection to prevent editing (movement, resize, rotation, etc.)
        targetShape.Protection.LockMoveX.Value = BOOL.True;
        targetShape.Protection.LockMoveY.Value = BOOL.True;
        targetShape.Protection.LockWidth.Value = BOOL.True;
        targetShape.Protection.LockHeight.Value = BOOL.True;
        targetShape.Protection.LockRotate.Value = BOOL.True;
        targetShape.Protection.LockDelete.Value = BOOL.True;
        targetShape.Protection.LockSelect.Value = BOOL.True;
        targetShape.Protection.LockFormat.Value = BOOL.True;
        targetShape.Protection.LockGroup.Value = BOOL.True;
        targetShape.Protection.LockVtxEdit.Value = BOOL.True;
        targetShape.Protection.LockCustProp.Value = BOOL.True;
        targetShape.Protection.LockThemeColors.Value = BOOL.True;
        targetShape.Protection.LockThemeEffects.Value = BOOL.True;
        targetShape.Protection.LockBegin.Value = BOOL.True;
        targetShape.Protection.LockEnd.Value = BOOL.True;
        targetShape.Protection.LockCalcWH.Value = BOOL.True;
        targetShape.Protection.LockCrop.Value = BOOL.True;
        targetShape.Protection.LockFromGroupFormat.Value = BOOL.True;
        targetShape.Protection.LockAspect.Value = BOOL.True;

        // Allow paragraph text editing while other edits are locked
        targetShape.Protection.LockTextEdit.Value = BOOL.False;

        // Verify that protection settings were applied correctly
        bool allLocked =
            targetShape.Protection.LockMoveX.Value == BOOL.True &&
            targetShape.Protection.LockMoveY.Value == BOOL.True &&
            targetShape.Protection.LockWidth.Value == BOOL.True &&
            targetShape.Protection.LockHeight.Value == BOOL.True &&
            targetShape.Protection.LockRotate.Value == BOOL.True &&
            targetShape.Protection.LockDelete.Value == BOOL.True &&
            targetShape.Protection.LockSelect.Value == BOOL.True &&
            targetShape.Protection.LockFormat.Value == BOOL.True &&
            targetShape.Protection.LockGroup.Value == BOOL.True &&
            targetShape.Protection.LockVtxEdit.Value == BOOL.True &&
            targetShape.Protection.LockCustProp.Value == BOOL.True &&
            targetShape.Protection.LockThemeColors.Value == BOOL.True &&
            targetShape.Protection.LockThemeEffects.Value == BOOL.True &&
            targetShape.Protection.LockBegin.Value == BOOL.True &&
            targetShape.Protection.LockEnd.Value == BOOL.True &&
            targetShape.Protection.LockCalcWH.Value == BOOL.True &&
            targetShape.Protection.LockCrop.Value == BOOL.True &&
            targetShape.Protection.LockFromGroupFormat.Value == BOOL.True &&
            targetShape.Protection.LockAspect.Value == BOOL.True &&
            targetShape.Protection.LockTextEdit.Value == BOOL.False;

        if (!allLocked)
        {
            throw new Exception("Protection settings verification failed.");
        }
        else
        {
            Console.WriteLine("Shape protection applied and verified successfully.");
        }

        // Define output file path
        string outputPath = "output_protected.vsdx";

        try
        {
            // Save the modified diagram with protection applied
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}