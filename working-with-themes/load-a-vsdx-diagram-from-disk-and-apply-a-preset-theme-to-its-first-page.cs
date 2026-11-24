using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> [outputVsdxPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_themed.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0).
                Page firstPage = diagram.Pages[0];

                // Apply a preset theme to the page.
                // Valid preset theme value: PresetThemeValue.Bubble
                firstPage.PresetTheme = PresetThemeValue.Bubble;

                // Optionally, a variant can be set (e.g., Variant1). Uncomment if needed.
                // firstPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the modified diagram to the output file in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Theme applied successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}