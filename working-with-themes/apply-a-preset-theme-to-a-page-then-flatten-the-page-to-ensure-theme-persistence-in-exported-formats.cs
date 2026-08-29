using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (theme will be persisted here)
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (you can select a different page by index or name)
            Page page = diagram.Pages[0];

            // Apply a preset theme to the page
            page.PresetTheme = PresetThemeValue.Bubble;               // set the theme
            page.PresetThemeVariant = PresetThemeVariantValue.Variant2; // optional variant

            // Save the diagram; the applied theme is persisted in the saved file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Theme applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}