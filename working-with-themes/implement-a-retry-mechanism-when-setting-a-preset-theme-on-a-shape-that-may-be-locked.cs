using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page and a shape with ID 1
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(1);

                // Apply the preset theme with retry logic
                const int maxAttempts = 3;
                bool success = ApplyPresetThemeWithRetry(shape, maxAttempts);

                if (!success)
                {
                    throw new Exception($"Failed to apply preset theme after {maxAttempts} attempts.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Attempts to set a preset theme on a shape, retrying after unlocking protection if needed.
        /// </summary>
        /// <param name="shape">The target shape.</param>
        /// <param name="maxAttempts">Maximum number of attempts.</param>
        /// <returns>True if the theme was applied successfully; otherwise false.</returns>
        private static bool ApplyPresetThemeWithRetry(Shape shape, int maxAttempts)
        {
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    // Set the preset theme and variant (write‑only properties)
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                    // Optionally set a quick style
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                    // If no exception, the operation succeeded
                    return true;
                }
                catch (Exception ex)
                {
                    // Log the failure (console output for this example)
                    Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                    // Unlock possible protection flags that could prevent theme changes
                    UnlockShapeProtection(shape);
                }
            }

            // All attempts failed
            return false;
        }

        /// <summary>
        /// Clears common protection flags on a shape to allow modifications.
        /// </summary>
        /// <param name="shape">The shape to unlock.</param>
        private static void UnlockShapeProtection(Shape shape)
        {
            // Unlock theme‑related protection
            shape.Protection.LockThemeColors.Value = BOOL.False;
            shape.Protection.LockThemeEffects.Value = BOOL.False;

            // Unlock other typical protection flags that might interfere
            shape.Protection.LockMoveX.Value = BOOL.False;
            shape.Protection.LockMoveY.Value = BOOL.False;
            shape.Protection.LockWidth.Value = BOOL.False;
            shape.Protection.LockHeight.Value = BOOL.False;
            shape.Protection.LockRotate.Value = BOOL.False;
            shape.Protection.LockDelete.Value = BOOL.False;
            shape.Protection.LockFormat.Value = BOOL.False;
            shape.Protection.LockSelect.Value = BOOL.False;
            shape.Protection.LockTextEdit.Value = BOOL.False;
        }
    }