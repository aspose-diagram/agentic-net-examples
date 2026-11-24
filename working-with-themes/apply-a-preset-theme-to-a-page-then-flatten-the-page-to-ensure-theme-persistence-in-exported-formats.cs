using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path; if not provided, create a default one next to the input file
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Themed.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Apply a preset theme to the first page
            Page page = diagram.Pages[0];
            page.PresetTheme = PresetThemeValue.Bubble;               // Set the theme
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1; // Optional variant

            // Note: Aspose.Diagram does not provide a Flatten method.
            // Theme changes are persisted automatically when the diagram is saved.

            // Save the modified diagram to VSDX format to retain the theme
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"The diagram with the applied theme has been saved to: {outputPath}");

            // Optional: Export to PDF to demonstrate that the theme persists in other formats
            string pdfPath = Path.ChangeExtension(outputPath, ".pdf");
            diagram.Save(pdfPath, SaveFileFormat.Pdf);
            Console.WriteLine($"The diagram exported to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}