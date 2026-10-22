using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Create PDF save options and configure them.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Explicitly set the target format on the options object.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Assign a custom callback to receive per‑page saving progress.
            pdfOptions.PageSavingCallback = new PdfPageSavingCallback();

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during PDF conversion: {ex.Message}");
        }
    }

    // Implementation of IPageSavingCallback to receive progress notifications.
    private class PdfPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has finished saving.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        }
    }
}