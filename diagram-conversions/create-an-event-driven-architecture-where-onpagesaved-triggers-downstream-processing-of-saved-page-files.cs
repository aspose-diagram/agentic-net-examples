using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageProcessing
{
    // Callback that is invoked after each page is saved during PDF export.
    class PageSavedCallback : IPageSavingCallback
    {
        private readonly string _outputDirectory;
        private readonly string _baseFileName;

        public PageSavedCallback(string outputDirectory, string baseFileName)
        {
            _outputDirectory = outputDirectory;
            _baseFileName = baseFileName;
        }

        // Called before a page starts saving – not used here.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No pre‑processing required.
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            int pageIndex = args.PageIndex; // zero‑based index
            int totalPages = args.PageCount;

            // Example downstream processing: log and define a per‑page file name.
            string pageFileName = $"{_baseFileName}_Page{pageIndex + 1}.pdf";
            string pageFilePath = Path.Combine(_outputDirectory, pageFileName);

            Console.WriteLine($"Page {pageIndex + 1}/{totalPages} saved. Downstream processing can use: {pageFilePath}");

            // Insert custom downstream logic here (e.g., move the file, upload, etc.).
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed).
                string inputPath = "input.vsdx";

                // Directory where downstream files will be referenced.
                string outputDir = "ProcessedPages";
                Directory.CreateDirectory(outputDir);

                // Output PDF file that contains the whole diagram.
                string outputPdfPath = Path.Combine(outputDir, "FullDiagram.pdf");

                // Load the diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Example: set a default font to avoid missing font warnings.
                        DefaultFont = "Arial"
                    };

                    // Attach the page‑saved callback.
                    pdfOptions.PageSavingCallback = new PageSavedCallback(outputDir, "FullDiagram");

                    // Save the diagram as PDF; the callback will be invoked per page.
                    diagram.Save(outputPdfPath, pdfOptions);
                }

                Console.WriteLine("Diagram export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}