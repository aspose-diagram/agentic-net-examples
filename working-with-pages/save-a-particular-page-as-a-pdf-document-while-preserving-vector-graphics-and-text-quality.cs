using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed or pass via command line)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output PDF file path
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine which page to export (0‑based index). Change as required.
            int pageIndex = 0;
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page index {pageIndex}. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Configure PDF save options for a single page export
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Export only the specified page
            pdfOptions.PageIndex = pageIndex;
            pdfOptions.PageCount = 1;
            // Do not export hidden pages
            pdfOptions.ExportHiddenPage = false;
            // Apply text compression to reduce file size while keeping vector quality
            pdfOptions.TextCompression = PdfTextCompression.Flate;

            // Save the selected page as a PDF preserving vector graphics and text quality
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Page {pageIndex + 1} saved to PDF successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}