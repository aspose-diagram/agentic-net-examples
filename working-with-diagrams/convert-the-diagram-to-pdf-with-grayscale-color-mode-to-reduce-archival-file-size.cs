using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramToPdfGrayscale
{
    // Custom callback (optional) – can be used for logging page save events.
    class PdfPageSavingCallback : Aspose.Diagram.Saving.IPageSavingCallback
    {
        public void PageStartSaving(Aspose.Diagram.Saving.PageStartSavingArgs args)
        {
            // Example: log start of page saving.
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(Aspose.Diagram.Saving.PageEndSavingArgs args)
        {
            // Example: log completion of page saving.
            Console.WriteLine($"Finished saving page {args.PageIndex + 1}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path – replace with your actual file.
                string inputPath = "input.vsdx";

                // Output PDF file path.
                string outputPath = "output.pdf";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set default font to avoid missing font issues.
                pdfOptions.DefaultFont = "Arial";

                // Exclude hidden pages to reduce file size.
                pdfOptions.ExportHiddenPage = false;

                // Assign the page saving callback (optional, can be omitted if not needed).
                pdfOptions.PageSavingCallback = new PdfPageSavingCallback();

                // NOTE:
                // Aspose.Diagram does not provide a direct property to force grayscale rendering.
                // Grayscale conversion would need to be performed after PDF generation using a PDF library.
                // The code below saves the diagram to PDF with the available options.

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram has been saved to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}