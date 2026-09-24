using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page and the first shape
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(1); // shape ID 1 for example

                // Desired theme settings
                PresetThemeValue theme = PresetThemeValue.Bubble;
                PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;
                PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle1;

                // Apply the theme with retry logic
                ApplyPresetThemeWithRetry(shape, theme, variant, quickStyle, maxRetries: 3);

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
        /// The method temporarily clears protection locks, applies the theme, then restores original lock states.
        /// </summary>
        static void ApplyPresetThemeWithRetry(Shape shape,
                                              PresetThemeValue theme,
                                              PresetThemeVariantValue variant,
                                              PresetQuickStyleValue quickStyle,
                                              int maxRetries)
        {
            // Store original lock values
            BOOL originalLockMoveX = shape.Protection.LockMoveX.Value;
            BOOL originalLockMoveY = shape.Protection.LockMoveY.Value;
            BOOL originalLockWidth = shape.Protection.LockWidth.Value;
            BOOL originalLockHeight = shape.Protection.LockHeight.Value;
            BOOL originalLockRotate = shape.Protection.LockRotate.Value;
            BOOL originalLockVtxEdit = shape.Protection.LockVtxEdit.Value;

            int attempt = 0;
            while (attempt < maxRetries)
            {
                try
                {
                    // Temporarily remove locks to allow theme change
                    shape.Protection.LockMoveX.Value = BOOL.False;
                    shape.Protection.LockMoveY.Value = BOOL.False;
                    shape.Protection.LockWidth.Value = BOOL.False;
                    shape.Protection.LockHeight.Value = BOOL.False;
                    shape.Protection.LockRotate.Value = BOOL.False;
                    shape.Protection.LockVtxEdit.Value = BOOL.False;

                    // Apply the preset theme and related properties
                    shape.PresetTheme = theme;
                    shape.PresetThemeVariant = variant;
                    shape.PresetThemeQuickStyle = quickStyle;

                    // If we reach this point, the operation succeeded
                    break;
                }
                catch (Exception ex)
                {
                    // Log the exception and retry
                    Console.WriteLine($"Attempt {attempt + 1} failed: {ex.Message}");
                    attempt++;

                    if (attempt >= maxRetries)
                    {
                        Console.WriteLine("Maximum retry attempts reached. Theme not applied.");
                    }
                }
                finally
                {
                    // Restore original lock states after each attempt
                    shape.Protection.LockMoveX.Value = originalLockMoveX;
                    shape.Protection.LockMoveY.Value = originalLockMoveY;
                    shape.Protection.LockWidth.Value = originalLockWidth;
                    shape.Protection.LockHeight.Value = originalLockHeight;
                    shape.Protection.LockRotate.Value = originalLockRotate;
                    shape.Protection.LockVtxEdit.Value = originalLockVtxEdit;
                }
            }
        }
    }