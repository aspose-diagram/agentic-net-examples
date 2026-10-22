using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Optional output path; if not provided, create a default output file name
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_unprotected.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // ---------- Clear document-level protection ----------
            diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
            diagram.DocumentSettings.ProtectMasters = BOOL.False;
            diagram.DocumentSettings.ProtectShapes = BOOL.False;
            diagram.DocumentSettings.ProtectStyles = BOOL.False;

            // ---------- Iterate through all pages and shapes ----------
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate over each shape in the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Clear all shape protection flags
                    ClearShapeProtection(shape);
                }
            }

            // ---------- Validation: ensure no shape protection flags remain set ----------
            bool anyLocked = false;
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    if (!ValidateShapeProtectionCleared(shape))
                    {
                        anyLocked = true;
                        Console.Error.WriteLine($"Shape ID {shape.ID} on page {pageIndex + 1} still has locked attributes.");
                    }
                }
            }

            if (anyLocked)
            {
                throw new Exception("Validation failed: some shapes still have locked attributes.");
            }

            // ---------- Save the unprotected diagram ----------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets all protection flags of a shape to FALSE.
    /// </summary>
    /// <param name="shape">The shape to modify.</param>
    private static void ClearShapeProtection(Shape shape)
    {
        // Each protection property is a BoolValue; set its Value to BOOL.False
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

    /// <summary>
    /// Checks that all protection flags of a shape are FALSE.
    /// Returns true if all are FALSE; otherwise false.
    /// </summary>
    /// <param name="shape">The shape to validate.</param>
    private static bool ValidateShapeProtectionCleared(Shape shape)
    {
        // Return false immediately if any flag is TRUE
        if (shape.Protection.LockAspect.Value == BOOL.True) return false;
        if (shape.Protection.LockBegin.Value == BOOL.True) return false;
        if (shape.Protection.LockCalcWH.Value == BOOL.True) return false;
        if (shape.Protection.LockCrop.Value == BOOL.True) return false;
        if (shape.Protection.LockCustProp.Value == BOOL.True) return false;
        if (shape.Protection.LockDelete.Value == BOOL.True) return false;
        if (shape.Protection.LockEnd.Value == BOOL.True) return false;
        if (shape.Protection.LockFormat.Value == BOOL.True) return false;
        if (shape.Protection.LockFromGroupFormat.Value == BOOL.True) return false;
        if (shape.Protection.LockGroup.Value == BOOL.True) return false;
        if (shape.Protection.LockHeight.Value == BOOL.True) return false;
        if (shape.Protection.LockMoveX.Value == BOOL.True) return false;
        if (shape.Protection.LockMoveY.Value == BOOL.True) return false;
        if (shape.Protection.LockRotate.Value == BOOL.True) return false;
        if (shape.Protection.LockSelect.Value == BOOL.True) return false;
        if (shape.Protection.LockTextEdit.Value == BOOL.True) return false;
        if (shape.Protection.LockThemeColors.Value == BOOL.True) return false;
        if (shape.Protection.LockThemeEffects.Value == BOOL.True) return false;
        if (shape.Protection.LockVtxEdit.Value == BOOL.True) return false;
        if (shape.Protection.LockWidth.Value == BOOL.True) return false;

        // All flags are FALSE
        return true;
    }
}