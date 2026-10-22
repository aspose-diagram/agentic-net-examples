using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        string outputDir = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath);

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists (create if necessary).
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page and export it as a separate HTML file,
            // preserving the original page layout.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the page (explicit type required).
                Page page = diagram.Pages[i];

                // Configure HTML save options for the current page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export only this page.
                    PageIndex = i,
                    PageCount = 1,

                    // Preserve layout by not exporting hidden pages.
                    ExportHiddenPage = false, // Changed from BOOL.False to bool

                    // Do not export comments.
                    IsExportComments = false // Changed from BOOL.False to bool
                };

                // Build the output file name (e.g., page_1.html).
                string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                // Save the page to HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Saved page {i + 1} to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
