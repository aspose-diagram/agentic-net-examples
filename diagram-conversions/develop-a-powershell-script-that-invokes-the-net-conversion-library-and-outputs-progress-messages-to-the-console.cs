using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the input Visio file path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: DiagramConverter <inputVisioPath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path; default to same folder with .pdf extension.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options and attach the page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new ProgressCallback();

            // Save the diagram to PDF while progress is reported via the callback.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Conversion completed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }

    // Implementation of IPageSavingCallback to report progress.
    private class ProgressCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example: stop after the first page (uncomment to use).
            // if (args.PageIndex == 0)
            // {
            //     args.HasMorePages = false;
            // }
        }
    }
}