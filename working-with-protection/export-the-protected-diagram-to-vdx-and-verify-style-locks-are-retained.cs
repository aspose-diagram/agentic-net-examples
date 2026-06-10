using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "protected_input.vsdx";
            string outputPath = "exported_output.vdx";

            // Load the protected diagram (Visio VSDX format)
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Capture protection lock states before export
            var preExportLocks = GetShapeProtectionLocks(diagram);

            // Export to VDX using DiagramSaveOptions
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            diagram.Save(outputPath, saveOptions);

            // Load the exported VDX file
            Diagram reloadedDiagram = new Diagram(outputPath, LoadFileFormat.Vdx);

            // Capture protection lock states after reloading
            var postExportLocks = GetShapeProtectionLocks(reloadedDiagram);

            // Verify that lock states are identical
            if (preExportLocks.Count != postExportLocks.Count)
            {
                throw new Exception("Lock verification failed: shape count mismatch after export.");
            }

            foreach (var kvp in preExportLocks)
            {
                if (!postExportLocks.TryGetValue(kvp.Key, out var postLocks))
                {
                    throw new Exception($"Lock verification failed: shape ID {kvp.Key} missing after export.");
                }

                if (!LocksAreEqual(kvp.Value, postLocks))
                {
                    throw new Exception($"Lock verification failed: lock state mismatch for shape ID {kvp.Key}.");
                }
            }

            Console.WriteLine("Export to VDX completed successfully. All style locks are retained.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Retrieves a dictionary mapping shape IDs to their protection lock states
    private static Dictionary<long, ShapeProtectionState> GetShapeProtectionLocks(Diagram diagram)
    {
        var result = new Dictionary<long, ShapeProtectionState>();

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Only consider shapes that are not deleted
                if (shape.Del == BOOL.True)
                    continue;

                var state = new ShapeProtectionState
                {
                    LockMoveX = shape.Protection.LockMoveX.Value,
                    LockMoveY = shape.Protection.LockMoveY.Value,
                    LockWidth = shape.Protection.LockWidth.Value,
                    LockHeight = shape.Protection.LockHeight.Value,
                    LockRotate = shape.Protection.LockRotate.Value,
                    LockVtxEdit = shape.Protection.LockVtxEdit.Value
                };

                result[shape.ID] = state;
            }
        }

        return result;
    }

    // Compares two protection states for equality
    private static bool LocksAreEqual(ShapeProtectionState a, ShapeProtectionState b)
    {
        return a.LockMoveX == b.LockMoveX &&
               a.LockMoveY == b.LockMoveY &&
               a.LockWidth == b.LockWidth &&
               a.LockHeight == b.LockHeight &&
               a.LockRotate == b.LockRotate &&
               a.LockVtxEdit == b.LockVtxEdit;
    }

    // Simple container for protection lock values
    private class ShapeProtectionState
    {
        public BOOL LockMoveX { get; set; }
        public BOOL LockMoveY { get; set; }
        public BOOL LockWidth { get; set; }
        public BOOL LockHeight { get; set; }
        public BOOL LockRotate { get; set; }
        public BOOL LockVtxEdit { get; set; }
    }
}