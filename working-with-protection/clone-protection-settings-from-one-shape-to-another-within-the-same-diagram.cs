using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // IDs of the source (with desired protection) and target shapes
                long sourceShapeId = 1; // replace with actual source shape ID
                long targetShapeId = 2; // replace with actual target shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Retrieve source and target shapes
                Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
                Shape targetShape = page.Shapes.GetShape(targetShapeId);

                if (sourceShape == null)
                {
                    throw new Exception($"Source shape with ID {sourceShapeId} not found.");
                }

                if (targetShape == null)
                {
                    throw new Exception($"Target shape with ID {targetShapeId} not found.");
                }

                // Clone protection settings from source to target
                targetShape.Protection.LockMoveX.Value = sourceShape.Protection.LockMoveX.Value;
                targetShape.Protection.LockMoveY.Value = sourceShape.Protection.LockMoveY.Value;
                targetShape.Protection.LockWidth.Value = sourceShape.Protection.LockWidth.Value;
                targetShape.Protection.LockHeight.Value = sourceShape.Protection.LockHeight.Value;
                targetShape.Protection.LockRotate.Value = sourceShape.Protection.LockRotate.Value;
                targetShape.Protection.LockVtxEdit.Value = sourceShape.Protection.LockVtxEdit.Value;
                targetShape.Protection.LockBegin.Value = sourceShape.Protection.LockBegin.Value;
                targetShape.Protection.LockEnd.Value = sourceShape.Protection.LockEnd.Value;
                targetShape.Protection.LockCalcWH.Value = sourceShape.Protection.LockCalcWH.Value;
                targetShape.Protection.LockCrop.Value = sourceShape.Protection.LockCrop.Value;
                targetShape.Protection.LockDelete.Value = sourceShape.Protection.LockDelete.Value;
                targetShape.Protection.LockFormat.Value = sourceShape.Protection.LockFormat.Value;
                targetShape.Protection.LockFromGroupFormat.Value = sourceShape.Protection.LockFromGroupFormat.Value;
                targetShape.Protection.LockGroup.Value = sourceShape.Protection.LockGroup.Value;
                targetShape.Protection.LockHeight.Value = sourceShape.Protection.LockHeight.Value;
                targetShape.Protection.LockSelect.Value = sourceShape.Protection.LockSelect.Value;
                targetShape.Protection.LockTextEdit.Value = sourceShape.Protection.LockTextEdit.Value;
                targetShape.Protection.LockThemeColors.Value = sourceShape.Protection.LockThemeColors.Value;
                targetShape.Protection.LockThemeEffects.Value = sourceShape.Protection.LockThemeEffects.Value;
                targetShape.Protection.LockCustProp.Value = sourceShape.Protection.LockCustProp.Value;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }