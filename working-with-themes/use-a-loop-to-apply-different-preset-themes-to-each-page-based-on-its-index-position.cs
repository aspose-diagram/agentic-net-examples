using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Loop through each page and apply a preset theme based on its index
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Example: apply the Bubble theme to all pages.
                // TODO: Replace with different PresetThemeValue members when available.
                page.PresetTheme = PresetThemeValue.Bubble;

                // If variant support is needed, set it here (ensure the enum value exists).
                // TODO: page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                Console.WriteLine($"Applied theme to page {pageIndex + 1}");
                pageIndex++;
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}