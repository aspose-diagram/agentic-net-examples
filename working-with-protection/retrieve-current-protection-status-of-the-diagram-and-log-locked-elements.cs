using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // ----- Global document protection status -----
            Console.WriteLine("=== Global Document Protection ===");
            Console.WriteLine($"Protect Backgrounds: {diagram.DocumentSettings.ProtectBkgnds}");
            Console.WriteLine($"Protect Masters: {diagram.DocumentSettings.ProtectMasters}");
            Console.WriteLine($"Protect Shapes: {diagram.DocumentSettings.ProtectShapes}");
            Console.WriteLine($"Protect Styles: {diagram.DocumentSettings.ProtectStyles}");
            Console.WriteLine();

            // ----- Shape-level protection status -----
            Console.WriteLine("=== Locked Shapes ===");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    var protection = shape.Protection;
                    bool anyLock = false;
                    System.Text.StringBuilder lockedProps = new System.Text.StringBuilder();

                    // Check each lock property
                    if (protection.LockAspect.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockAspect, "); }
                    if (protection.LockBegin.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockBegin, "); }
                    if (protection.LockCalcWH.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockCalcWH, "); }
                    if (protection.LockCrop.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockCrop, "); }
                    if (protection.LockCustProp.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockCustProp, "); }
                    if (protection.LockDelete.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockDelete, "); }
                    if (protection.LockEnd.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockEnd, "); }
                    if (protection.LockFormat.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockFormat, "); }
                    if (protection.LockFromGroupFormat.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockFromGroupFormat, "); }
                    if (protection.LockGroup.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockGroup, "); }
                    if (protection.LockHeight.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockHeight, "); }
                    if (protection.LockMoveX.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockMoveX, "); }
                    if (protection.LockMoveY.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockMoveY, "); }
                    if (protection.LockRotate.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockRotate, "); }
                    if (protection.LockSelect.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockSelect, "); }
                    if (protection.LockTextEdit.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockTextEdit, "); }
                    if (protection.LockThemeColors.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockThemeColors, "); }
                    if (protection.LockThemeEffects.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockThemeEffects, "); }
                    if (protection.LockVtxEdit.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockVtxEdit, "); }
                    if (protection.LockWidth.Value == BOOL.True) { anyLock = true; lockedProps.Append("LockWidth, "); }

                    if (anyLock)
                    {
                        // Remove trailing comma and space
                        if (lockedProps.Length >= 2)
                            lockedProps.Length -= 2;

                        Console.WriteLine($"Page: {page.Name} | Shape ID: {shape.ID} | Name: {shape.Name} | Locked: {lockedProps}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}