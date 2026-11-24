using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the Visio file path
        Console.Write("Enter the path to the Visio diagram (e.g., diagram.vsdx): ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the file exists
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Error: The specified diagram file does not exist.");
            return;
        }

        // Prompt for source page number (1‑based for user friendliness)
        Console.Write("Enter the source page number (starting from 1): ");
        if (!int.TryParse(Console.ReadLine(), out int sourcePageNumber) || sourcePageNumber < 1)
        {
            Console.Error.WriteLine("Error: Invalid source page number.");
            return;
        }

        // Prompt for target page number
        Console.Write("Enter the target page number (starting from 1): ");
        if (!int.TryParse(Console.ReadLine(), out int targetPageNumber) || targetPageNumber < 1)
        {
            Console.Error.WriteLine("Error: Invalid target page number.");
            return;
        }

        // Prompt for output file path
        Console.Write("Enter the output path for the modified diagram (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();

        // Guard: ensure output directory exists
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Error: Output path is empty.");
            return;
        }
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine("Error: The directory for the output file does not exist.");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page indices against the diagram's page count
                int pageCount = diagram.Pages.Count;
                if (sourcePageNumber > pageCount || targetPageNumber > pageCount)
                {
                    Console.Error.WriteLine($"Error: Diagram contains {pageCount} pages. Specified page numbers are out of range.");
                    return;
                }

                // Convert to zero‑based indices
                int sourceIndex = sourcePageNumber - 1;
                int targetIndex = targetPageNumber - 1;

                // Retrieve source and target pages
                Page sourcePage = diagram.Pages[sourceIndex];
                Page targetPage = diagram.Pages[targetIndex];

                // -----------------------------------------------------------------
                // Clone theme settings from source to target.
                // NOTE: Page.PresetTheme and Page.PresetThemeVariant are write‑only
                // properties; the API does not provide getters to read the current
                // values. Therefore, the exact theme cannot be retrieved directly.
                // -----------------------------------------------------------------
                // TODO: Replace the placeholder values with the actual theme settings
                // from the source page when a read accessor becomes available.
                // For now we assign a default theme as an example.
                targetPage.PresetTheme = PresetThemeValue.Bubble;
                targetPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Theme (placeholder) applied to page {targetPageNumber} and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}