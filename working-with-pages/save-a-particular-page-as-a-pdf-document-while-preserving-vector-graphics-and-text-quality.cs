using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file, output PDF file, optional page index
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile> [pageIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int pageIndex = 0; // default to first page (zero‑based)

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse optional page index argument
        if (args.Length >= 3)
        {
            if (!int.TryParse(args[2], out pageIndex) || pageIndex < 0)
            {
                Console.Error.WriteLine("Invalid page index. It must be a non‑negative integer.");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate requested page index against the document's page count
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Document contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Configure PDF save options to export only the specified page
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export only the desired page (zero‑based index)
                pdfOptions.PageIndex = pageIndex;
                pdfOptions.PageCount = 1;

                // Preserve vector graphics and text quality (default behavior)
                // Additional options:
                pdfOptions.ExportHiddenPage = false; // do not export hidden pages

                // Optional: set a fallback font in case the diagram uses missing fonts
                pdfOptions.DefaultFont = "Arial";

                // Save the selected page as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Page {pageIndex} saved successfully to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}
