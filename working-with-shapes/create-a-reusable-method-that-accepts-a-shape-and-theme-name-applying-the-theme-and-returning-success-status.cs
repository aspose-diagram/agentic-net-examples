using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    /// <summary>
    /// Applies a preset theme to the specified shape.
    /// Returns true if the theme was applied successfully; otherwise false.
    /// </summary>
    /// <param name="shape">The shape to which the theme will be applied.</param>
    /// <param name="themeName">Name of the theme (case‑insensitive).</param>
    /// <returns>Boolean indicating success.</returns>
    static bool ApplyThemeToShape(Shape shape, string themeName)
    {
        // Guard against a null shape reference.
        if (shape == null)
        {
            Console.Error.WriteLine("Shape is null.");
            return false;
        }

        // Skip shapes that are marked as deleted.
        if (shape.Del == BOOL.True)
        {
            Console.Error.WriteLine("Shape is marked as deleted.");
            return false;
        }

        // Map the supplied theme name to a PresetThemeValue enum.
        // Only the confirmed enum members are supported to avoid compilation errors.
        PresetThemeValue presetTheme;
        switch (themeName?.Trim().ToLowerInvariant())
        {
            case "bubble":
                presetTheme = PresetThemeValue.Bubble;
                break;
            case "clouds":
                presetTheme = PresetThemeValue.Clouds;
                break;
            default:
                Console.Error.WriteLine($"Unsupported or unknown theme name: '{themeName}'.");
                return false;
        }

        try
        {
            // Apply the preset theme to the shape.
            shape.PresetTheme = presetTheme;

            // Optionally set a variant and quick style (using the first available values).
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Theme applied successfully.
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error applying theme: {ex.Message}");
            return false;
        }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page firstPage = diagram.Pages[0];

                // Verify that the page contains at least one shape.
                if (firstPage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = firstPage.Shapes[0];

                // Example theme name; could be replaced with a command‑line argument or other source.
                string themeName = "Bubble";

                // Apply the theme using the reusable method.
                bool success = ApplyThemeToShape(shape, themeName);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to apply the theme to the shape.");
                    return;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}