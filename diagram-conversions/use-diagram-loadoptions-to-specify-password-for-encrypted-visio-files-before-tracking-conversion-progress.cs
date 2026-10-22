using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ProgressCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // If you need to stop further processing, set:
        // args.HasMorePages = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (encrypted file handling not supported via LoadOptions in this version).
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF file path.
        string outputPath = "output.pdf";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create LoadOptions instance (no password property available).
            LoadOptions loadOptions = new LoadOptions();

            // Load the diagram using the LoadOptions.
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Prepare PDF save options and assign the progress callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new ProgressCallback();

            // Explicitly set the save format for the options.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram to PDF while tracking progress.
            diagram.Save(outputPath, pdfOptions);

            // Dispose the diagram to free resources.
            diagram.Dispose();

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}