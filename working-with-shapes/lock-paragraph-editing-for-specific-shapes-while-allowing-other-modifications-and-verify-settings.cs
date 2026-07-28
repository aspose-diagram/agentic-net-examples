using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the names of shapes whose paragraph editing should be locked
                string[] shapesToLock = { "LockedShape1", "LockedShape2" };

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape name matches one of the target shapes
                        bool shouldLock = false;
                        foreach (string targetName in shapesToLock)
                        {
                            if (string.Equals(shape.NameU, targetName, StringComparison.OrdinalIgnoreCase))
                            {
                                shouldLock = true;
                                break;
                            }
                        }

                        if (!shouldLock)
                            continue;

                        // Apply lock to text editing (paragraph editing) only
                        shape.Protection.LockTextEdit.Value = BOOL.True;

                        // Ensure other protection flags remain unlocked (allow other modifications)
                        shape.Protection.LockMoveX.Value = BOOL.False;
                        shape.Protection.LockMoveY.Value = BOOL.False;
                        shape.Protection.LockWidth.Value = BOOL.False;
                        shape.Protection.LockHeight.Value = BOOL.False;
                        shape.Protection.LockRotate.Value = BOOL.False;
                        shape.Protection.LockDelete.Value = BOOL.False;
                        shape.Protection.LockFormat.Value = BOOL.False;
                        shape.Protection.LockSelect.Value = BOOL.False;
                        shape.Protection.LockAspect.Value = BOOL.False;
                        shape.Protection.LockVtxEdit.Value = BOOL.False;
                        shape.Protection.LockCustProp.Value = BOOL.False;
                        shape.Protection.LockBegin.Value = BOOL.False;
                        shape.Protection.LockEnd.Value = BOOL.False;
                        shape.Protection.LockCalcWH.Value = BOOL.False;
                        shape.Protection.LockCrop.Value = BOOL.False;
                        shape.Protection.LockFromGroupFormat.Value = BOOL.False;
                        shape.Protection.LockGroup.Value = BOOL.False;
                        shape.Protection.LockThemeColors.Value = BOOL.False;
                        shape.Protection.LockThemeEffects.Value = BOOL.False;

                        // Verification
                        if (shape.Protection.LockTextEdit.Value != BOOL.True)
                        {
                            throw new Exception($"Failed to lock paragraph editing for shape ID {shape.ID} (NameU: {shape.NameU}).");
                        }

                        // Verify that other locks are indeed false
                        if (shape.Protection.LockMoveX.Value != BOOL.False ||
                            shape.Protection.LockMoveY.Value != BOOL.False ||
                            shape.Protection.LockWidth.Value != BOOL.False ||
                            shape.Protection.LockHeight.Value != BOOL.False ||
                            shape.Protection.LockRotate.Value != BOOL.False)
                        {
                            throw new Exception($"Unexpected protection settings on shape ID {shape.ID} (NameU: {shape.NameU}).");
                        }

                        Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) locked for paragraph editing successfully.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }