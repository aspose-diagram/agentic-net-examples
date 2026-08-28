using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <pageIndexZeroBased> <outputPdfPath>");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Parse page index (zero‑based).
        if (!int.TryParse(args[1], out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index. It must be a non‑negative integer.");
            return;
        }

        // Output PDF file path.
        string outputPath = args[2];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Verify the requested page exists.
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Configure PDF save options to export only the selected page.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Explicitly set the format tracker (required to avoid ambiguity).
                SaveFormat = SaveFileFormat.Pdf,
                // Export a single page starting at the requested index.
                PageIndex = pageIndex,
                PageCount = 1,
                // Optional: set a default font to ensure text rendering if the original font is missing.
                DefaultFont = "Arial",
                // Optional: do not include hidden pages in the output.
                ExportHiddenPage = false
            };

            // Save the selected page as a PDF file.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Page {pageIndex} successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}