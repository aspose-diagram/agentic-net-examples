using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Callback to report progress for each page during PDF saving
    public class PageProgressCallback : IPageSavingCallback
    {
        private readonly int _totalPages;
        private int _processedPages = 0;

        public PageProgressCallback(int totalPages)
        {
            _totalPages = totalPages;
        }

        // Called before a page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start
        }

        // Called after a page is saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            _processedPages++;
            double percent = (double)_processedPages / _totalPages * 100;
            Console.WriteLine($"Saving progress: {_processedPages}/{_totalPages} pages ({percent:F1}%)");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Determine total number of pages
                int pageCount = diagram.Pages.Count;

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure a default font is set to avoid missing font issues
                    DefaultFont = "Arial"
                };

                // Assign the progress callback
                pdfOptions.PageSavingCallback = new PageProgressCallback(pageCount);

                // Save the diagram as PDF with progress reporting
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Conversion completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}