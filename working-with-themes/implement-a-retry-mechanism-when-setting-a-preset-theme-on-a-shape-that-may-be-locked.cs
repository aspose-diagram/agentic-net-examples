using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    targetShape = shp;
                    break;
                }

                if (targetShape == null)
                {
                    throw new Exception("No shape found on the first page.");
                }

                // Apply preset theme with retry logic
                const int maxRetries = 3;
                ApplyPresetThemeWithRetry(targetShape, maxRetries);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Attempts to set a preset theme on a shape, retrying if the shape is locked.
        /// </summary>
        /// <param name="shape">The shape to modify.</param>
        /// <param name="maxRetries">Maximum number of retry attempts.</param>
        private static void ApplyPresetThemeWithRetry(Shape shape, int maxRetries)
        {
            // Preserve original lock states to restore later
            BOOL originalLockThemeColors = shape.Protection.LockThemeColors.Value;
            BOOL originalLockThemeEffects = shape.Protection.LockThemeEffects.Value;

            int attempt = 0;
            while (true)
            {
                try
                {
                    // Unlock theme-related protection if necessary
                    if (shape.Protection.LockThemeColors.Value == BOOL.True)
                    {
                        shape.Protection.LockThemeColors.Value = BOOL.False;
                    }
                    if (shape.Protection.LockThemeEffects.Value == BOOL.True)
                    {
                        shape.Protection.LockThemeEffects.Value = BOOL.False;
                    }

                    // Set the preset theme properties
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                    // Theme applied successfully; exit loop
                    break;
                }
                catch (Exception ex)
                {
                    attempt++;
                    if (attempt >= maxRetries)
                    {
                        // Restore original lock states before rethrowing
                        shape.Protection.LockThemeColors.Value = originalLockThemeColors;
                        shape.Protection.LockThemeEffects.Value = originalLockThemeEffects;
                        throw new Exception($"Failed to apply preset theme after {maxRetries} attempts.", ex);
                    }

                    // Optionally, you could add a small delay here before retrying
                }
            }

            // Restore original lock states after successful application
            shape.Protection.LockThemeColors.Value = originalLockThemeColors;
            shape.Protection.LockThemeEffects.Value = originalLockThemeEffects;
        }
    }