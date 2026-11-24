using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page and one shape.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page firstPage = diagram.Pages[0];
                if (firstPage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape as the target for theme application.
                Shape shape = firstPage.Shapes[0];

                // Define the preset theme values to apply.
                PresetThemeValue theme = PresetThemeValue.Bubble;
                PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;
                PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle1;

                const int maxAttempts = 3;
                int attempt = 0;
                bool success = false;

                while (attempt < maxAttempts && !success)
                {
                    attempt++;
                    try
                    {
                        // Attempt to set the theme properties on the shape.
                        shape.PresetTheme = theme;
                        shape.PresetThemeVariant = variant;
                        shape.PresetThemeQuickStyle = quickStyle;

                        // If no exception, the operation succeeded.
                        success = true;
                        Console.WriteLine($"Theme applied successfully on attempt {attempt}.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                        // Unlock possible theme‑related protection cells.
                        try
                        {
                            // Only valid protection properties are unlocked.
                            shape.Protection.LockThemeColors.Value = BOOL.False;
                            shape.Protection.LockThemeEffects.Value = BOOL.False;
                            // Removed invalid properties LockThemeFonts and LockThemeIndex.
                        }
                        catch (Exception unlockEx)
                        {
                            // Log unlocking errors but continue retrying.
                            Console.Error.WriteLine($"Failed to modify lock cells: {unlockEx.Message}");
                        }

                        // If this was the final attempt, rethrow to be caught by outer handler.
                        if (attempt >= maxAttempts)
                        {
                            throw;
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception e)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {e.Message}");
        }
    }
}
