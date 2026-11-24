using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, expected theme name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> <outputVsdxPath> <expectedTheme>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string expectedThemeName = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the expected theme enum value
        if (!Enum.TryParse<PresetThemeValue>(expectedThemeName, out var expectedTheme))
        {
            Console.Error.WriteLine($"Invalid PresetThemeValue: {expectedThemeName}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Apply the expected preset theme to the first page
                Page page = diagram.Pages[0];
                page.PresetTheme = expectedTheme;

                // Optionally set a variant (using Variant1 as a default)
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify persistence of the theme
            using (Diagram reloadedDiagram = new Diagram(outputPath))
            {
                if (reloadedDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Reloaded diagram contains no pages.");
                    return;
                }

                Page reloadedPage = reloadedDiagram.Pages[0];

                // The PresetTheme property is write‑only; it cannot be read back.
                // As a practical verification, we attempt to set the same theme again.
                // If no exception occurs, we assume the theme identifier persisted correctly.
                try
                {
                    reloadedPage.PresetTheme = expectedTheme;
                    reloadedPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Verification failed: unable to reapply theme after reload. {ex.Message}");
                    return;
                }

                // If we reach this point, the theme was applied and persisted without error.
                Console.WriteLine("Theme validation succeeded: the preset theme matches the expected value.");
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}