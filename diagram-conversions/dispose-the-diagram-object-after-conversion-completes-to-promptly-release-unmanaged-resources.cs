using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram? diagram = new Diagram(inputPath); // Made nullable to allow null assignment later.

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Export hidden pages if any.
                ExportHiddenPage = true, // Corrected type to bool.
                // Set PDF/A compliance level.
                Compliance = PdfCompliance.PdfA1b,
                // Specify a fallback font for missing fonts.
                DefaultFont = "Arial"
            };

            // Assign a custom page-saving callback to track progress.
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            // Release the diagram reference for garbage collection.
            diagram = null;
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    // Implementation of the page-saving callback to monitor per-page processing.
    private class CustomPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
        }

        // Called after a page has finished saving.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
            // To stop further pages, uncomment the following line.
            // args.HasMorePages = false; // 'HasMorePages' expects a plain bool, not BOOL.
        }
    }
}
