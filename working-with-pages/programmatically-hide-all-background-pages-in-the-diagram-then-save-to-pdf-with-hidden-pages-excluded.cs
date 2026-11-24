using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (allow command‑line overrides)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and hide those marked as background pages
                foreach (Page page in diagram.Pages)
                {
                    // A background page is identified by the Background flag (BOOL)
                    if (page.Background == BOOL.True)
                    {
                        // Hide the page by setting UIVisibility to Hidden
                        page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                    }
                }

                // Configure PDF save options to exclude hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Do not export hidden pages
                    ExportHiddenPage = false,
                    // Explicitly set the format tracker
                    SaveFormat = SaveFileFormat.Pdf,
                    // Optional: apply text compression for smaller PDF size
                    TextCompression = PdfTextCompression.Flate
                };

                // Save the diagram as PDF using the configured options
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram saved to PDF successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
