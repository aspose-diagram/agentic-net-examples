using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file path.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output PDF file path.
        string outputPath = "second_page.pdf";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options to export only the second page (zero‑based index 1).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Export a single page starting from the second page.
            pdfOptions.PageIndex = 1;   // Zero‑based page index.
            pdfOptions.PageCount = 1;   // Export only one page.
            // Do not export hidden pages.
            pdfOptions.ExportHiddenPage = false; // Use native bool, not BOOL enum.

            // Save the selected page to PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Second page exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
