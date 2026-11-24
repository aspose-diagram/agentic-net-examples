using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the preset theme and variant to apply to every page.
                // PresetThemeValue.Bubble is a valid theme; Variant1 is a common variant.
                PresetThemeValue theme = PresetThemeValue.Bubble;
                PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;

                // Iterate through all pages and assign the theme and variant.
                foreach (Page page in diagram.Pages)
                {
                    // Apply the preset theme.
                    page.PresetTheme = theme;

                    // Apply the preset theme variant (write‑only property).
                    page.PresetThemeVariant = variant;
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}