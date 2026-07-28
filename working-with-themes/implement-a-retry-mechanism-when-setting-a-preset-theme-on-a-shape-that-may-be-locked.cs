using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Applies a preset theme to a shape with retry logic.
    // If the shape is locked, temporarily unlocks the relevant protection cells,
    // applies the theme, then restores the original lock states.
    static void ApplyPresetThemeWithRetry(Shape shape, PresetThemeValue theme, int maxRetries = 3)
    {
        // Store original lock states
        var originalLocks = new System.Collections.Generic.Dictionary<string, BOOL>
        {
            { "LockMoveX", shape.Protection.LockMoveX.Value },
            { "LockMoveY", shape.Protection.LockMoveY.Value },
            { "LockWidth", shape.Protection.LockWidth.Value },
            { "LockHeight", shape.Protection.LockHeight.Value },
            { "LockRotate", shape.Protection.LockRotate.Value },
            { "LockVtxEdit", shape.Protection.LockVtxEdit.Value },
            { "LockThemeColors", shape.Protection.LockThemeColors.Value },
            { "LockThemeEffects", shape.Protection.LockThemeEffects.Value }
        };

        // Helper to unlock all relevant protection cells
        void UnlockAll()
        {
            shape.Protection.LockMoveX.Value = BOOL.False;
            shape.Protection.LockMoveY.Value = BOOL.False;
            shape.Protection.LockWidth.Value = BOOL.False;
            shape.Protection.LockHeight.Value = BOOL.False;
            shape.Protection.LockRotate.Value = BOOL.False;
            shape.Protection.LockVtxEdit.Value = BOOL.False;
            shape.Protection.LockThemeColors.Value = BOOL.False;
            shape.Protection.LockThemeEffects.Value = BOOL.False;
        }

        // Helper to restore original lock states
        void RestoreLocks()
        {
            shape.Protection.LockMoveX.Value = originalLocks["LockMoveX"];
            shape.Protection.LockMoveY.Value = originalLocks["LockMoveY"];
            shape.Protection.LockWidth.Value = originalLocks["LockWidth"];
            shape.Protection.LockHeight.Value = originalLocks["LockHeight"];
            shape.Protection.LockRotate.Value = originalLocks["LockRotate"];
            shape.Protection.LockVtxEdit.Value = originalLocks["LockVtxEdit"];
            shape.Protection.LockThemeColors.Value = originalLocks["LockThemeColors"];
            shape.Protection.LockThemeEffects.Value = originalLocks["LockThemeEffects"];
        }

        int attempt = 0;
        while (true)
        {
            attempt++;
            try
            {
                UnlockAll();
                shape.PresetTheme = theme; // Apply the theme
                RestoreLocks();
                Console.WriteLine($"Preset theme applied successfully on attempt {attempt}.");
                break; // Success
            }
            catch (Exception ex)
            {
                RestoreLocks(); // Ensure locks are restored even on failure
                Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                if (attempt >= maxRetries)
                {
                    throw new Exception($"Failed to apply preset theme after {maxRetries} attempts.", ex);
                }
                // Optional: wait or perform additional handling before retrying
            }
        }
    }

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page and first shape
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1); // Shape ID 1; adjust as needed

            // Apply the "Bubble" preset theme with retry logic
            ApplyPresetThemeWithRetry(shape, PresetThemeValue.Bubble);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
