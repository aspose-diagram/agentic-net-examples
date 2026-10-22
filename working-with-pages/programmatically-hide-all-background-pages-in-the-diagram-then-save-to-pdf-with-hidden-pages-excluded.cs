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
            // Load the diagram inside a using block as required by the API guidelines
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and hide those that are background pages
                foreach (Page page in diagram.Pages)
                {
                    // Identify a background page via the Background BOOL property
                    if (page.Background == BOOL.True)
                    {
                        // Hide the page by setting its UIVisibility to Hidden
                        page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                    }
                }

                // Configure PDF save options to exclude hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    ExportHiddenPage = false,               // Do not export hidden pages
                    SaveFormat = SaveFileFormat.Pdf         // Explicitly set the format tracker
                };

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}