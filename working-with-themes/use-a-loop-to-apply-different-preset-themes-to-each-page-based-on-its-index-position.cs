using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Themed_" + Path.GetFileName(inputPath));

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Loop through each page and apply a preset theme based on its index
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the page by index
                Page page = diagram.Pages[i];

                // Apply a preset theme (Bubble is a valid member of PresetThemeValue)
                page.PresetTheme = PresetThemeValue.Bubble;

                // Apply a variant to differentiate pages.
                // Cycle through Variant1, Variant2, Variant3 based on page index.
                // PresetThemeVariantValue enum is assumed to contain Variant1, Variant2, Variant3.
                switch (i % 3)
                {
                    case 0:
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        break;
                    case 1:
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                        break;
                    case 2:
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                        break;
                }

                Console.WriteLine($"Applied theme to page {i + 1}: PresetTheme=Bubble, Variant={(i % 3) + 1}");
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}