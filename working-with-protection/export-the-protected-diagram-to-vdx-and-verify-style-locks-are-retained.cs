using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Helper class to store protection lock states for a shape
    class ProtectionLocks
    {
        public BOOL LockMoveX { get; set; }
        public BOOL LockMoveY { get; set; }
        public BOOL LockWidth { get; set; }
        public BOOL LockHeight { get; set; }
        public BOOL LockRotate { get; set; }
        public BOOL LockVtxEdit { get; set; }
        public BOOL LockDelete { get; set; }
        public BOOL LockSelect { get; set; }
        public BOOL LockTextEdit { get; set; }
        public BOOL LockThemeColors { get; set; }
        public BOOL LockThemeEffects { get; set; }
        public BOOL LockCustProp { get; set; }

        // Capture all relevant lock values from a shape
        public static ProtectionLocks FromShape(Shape shape)
        {
            return new ProtectionLocks
            {
                LockMoveX = shape.Protection.LockMoveX.Value,
                LockMoveY = shape.Protection.LockMoveY.Value,
                LockWidth = shape.Protection.LockWidth.Value,
                LockHeight = shape.Protection.LockHeight.Value,
                LockRotate = shape.Protection.LockRotate.Value,
                LockVtxEdit = shape.Protection.LockVtxEdit.Value,
                LockDelete = shape.Protection.LockDelete.Value,
                LockSelect = shape.Protection.LockSelect.Value,
                LockTextEdit = shape.Protection.LockTextEdit.Value,
                LockThemeColors = shape.Protection.LockThemeColors.Value,
                LockThemeEffects = shape.Protection.LockThemeEffects.Value,
                LockCustProp = shape.Protection.LockCustProp.Value
            };
        }

        // Compare two lock sets; returns true if all match
        public bool Equals(ProtectionLocks other)
        {
            return other != null &&
                   LockMoveX == other.LockMoveX &&
                   LockMoveY == other.LockMoveY &&
                   LockWidth == other.LockWidth &&
                   LockHeight == other.LockHeight &&
                   LockRotate == other.LockRotate &&
                   LockVtxEdit == other.LockVtxEdit &&
                   LockDelete == other.LockDelete &&
                   LockSelect == other.LockSelect &&
                   LockTextEdit == other.LockTextEdit &&
                   LockThemeColors == other.LockThemeColors &&
                   LockThemeEffects == other.LockThemeEffects &&
                   LockCustProp == other.LockCustProp;
        }
    }

    static void Main(string[] args)
    {
        // Paths – adjust as needed
        string inputPath = "protected_input.vdx";
        // Guard to ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "exported_output.vdx";

        try
        {
            // Load the original protected diagram
            Diagram originalDiagram = new Diagram(inputPath, LoadFileFormat.Vdx);

            // Capture protection lock states for each shape in the original diagram
            var originalLocks = new Dictionary<long, ProtectionLocks>();
            foreach (Page page in originalDiagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Store by shape ID (unique across diagram)
                    originalLocks[shape.ID] = ProtectionLocks.FromShape(shape);
                }
            }

            // Export (save) the diagram to VDX format
            originalDiagram.Save(outputPath, SaveFileFormat.Vdx);

            // Load the exported VDX file
            Diagram exportedDiagram = new Diagram(outputPath, LoadFileFormat.Vdx);

            // Verify that style locks are retained
            foreach (Page page in exportedDiagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (!originalLocks.TryGetValue(shape.ID, out ProtectionLocks expectedLocks))
                    {
                        throw new Exception($"Shape ID {shape.ID} was not present in the original diagram.");
                    }

                    ProtectionLocks actualLocks = ProtectionLocks.FromShape(shape);
                    if (!actualLocks.Equals(expectedLocks))
                    {
                        throw new Exception($"Lock mismatch on shape ID {shape.ID}.");
                    }
                }
            }

            Console.WriteLine("Export to VDX completed successfully. All style locks are retained.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}