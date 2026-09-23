using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPdfExport
{
    // Callback implementation to receive page saving progress
    public class PageSavingProgressCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // Continue processing remaining pages
            // args.HasMorePages = false; // Uncomment to stop after first page
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;          // Explicitly set format
                pdfOptions.DefaultFont = "Arial";                    // Fallback font
                pdfOptions.PageSavingCallback = new PageSavingProgressCallback();

                // Save the diagram to PDF with progress callbacks
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram saved to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}