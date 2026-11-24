using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Step 1: Clear all shape protection flags.
            // -----------------------------------------------------------------
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                var page = diagram.Pages[pageIndex];

                // Iterate through each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Clear each protection flag by setting it to FALSE.
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
            }

            // -----------------------------------------------------------------
            // Step 2: Validate that no protection flags remain set to TRUE.
            // -----------------------------------------------------------------
            bool anyLocked = false;

            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                var page = diagram.Pages[pageIndex];

                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    // Helper local function to test a flag.
                    void CheckFlag(string flagName, BOOL flagValue)
                    {
                        if (flagValue == BOOL.True)
                        {
                            anyLocked = true;
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page {pageIndex} still has locked flag: {flagName}");
                        }
                    }

                    // Evaluate each protection flag.
                    CheckFlag(nameof(shape.Protection.LockAspect), shape.Protection.LockAspect.Value);
                    CheckFlag(nameof(shape.Protection.LockBegin), shape.Protection.LockBegin.Value);
                    CheckFlag(nameof(shape.Protection.LockCalcWH), shape.Protection.LockCalcWH.Value);
                    CheckFlag(nameof(shape.Protection.LockCrop), shape.Protection.LockCrop.Value);
                    CheckFlag(nameof(shape.Protection.LockCustProp), shape.Protection.LockCustProp.Value);
                    CheckFlag(nameof(shape.Protection.LockDelete), shape.Protection.LockDelete.Value);
                    CheckFlag(nameof(shape.Protection.LockEnd), shape.Protection.LockEnd.Value);
                    CheckFlag(nameof(shape.Protection.LockFormat), shape.Protection.LockFormat.Value);
                    CheckFlag(nameof(shape.Protection.LockFromGroupFormat), shape.Protection.LockFromGroupFormat.Value);
                    CheckFlag(nameof(shape.Protection.LockGroup), shape.Protection.LockGroup.Value);
                    CheckFlag(nameof(shape.Protection.LockHeight), shape.Protection.LockHeight.Value);
                    CheckFlag(nameof(shape.Protection.LockMoveX), shape.Protection.LockMoveX.Value);
                    CheckFlag(nameof(shape.Protection.LockMoveY), shape.Protection.LockMoveY.Value);
                    CheckFlag(nameof(shape.Protection.LockRotate), shape.Protection.LockRotate.Value);
                    CheckFlag(nameof(shape.Protection.LockSelect), shape.Protection.LockSelect.Value);
                    CheckFlag(nameof(shape.Protection.LockTextEdit), shape.Protection.LockTextEdit.Value);
                    CheckFlag(nameof(shape.Protection.LockThemeColors), shape.Protection.LockThemeColors.Value);
                    CheckFlag(nameof(shape.Protection.LockThemeEffects), shape.Protection.LockThemeEffects.Value);
                    CheckFlag(nameof(shape.Protection.LockVtxEdit), shape.Protection.LockVtxEdit.Value);
                    CheckFlag(nameof(shape.Protection.LockWidth), shape.Protection.LockWidth.Value);
                }
            }

            if (!anyLocked)
            {
                Console.WriteLine("All shape protection flags have been successfully cleared.");
            }
            else
            {
                Console.Error.WriteLine("Some shape protection flags remain set. See messages above.");
            }

            // -----------------------------------------------------------------
            // Step 3: Save the modified diagram.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}