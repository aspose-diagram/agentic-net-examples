using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Callback to track page saving progress during PDF export
    class PageProgressCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        }
    }

    class Program
    {
        static void Main()
        {
            string inputPath = "encrypted.vsdx";   // Path to the encrypted Visio file
            string outputPath = "output.pdf";      // Desired output PDF file

            Diagram diagram;
            try
            {
                // Load the diagram. Password handling is not supported via LoadOptions,
                // so the file must be accessible without a password or handled externally.
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Configure PDF save options with a progress callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial",                     // Fallback font
                SaveFormat = SaveFileFormat.Pdf,           // Explicitly set format
                PageSavingCallback = new PageProgressCallback()
            };

            // Save the diagram to PDF while tracking progress
            try
            {
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine("Diagram conversion completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }
}