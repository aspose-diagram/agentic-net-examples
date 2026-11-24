using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file and output PDF file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker (required for PDF export)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Enable text compression to reduce PDF size (optional)
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Assign the custom page-saving callback to handle per-page events
                pdfOptions.PageSavingCallback = new MyPageSavingCallback();

                // Save the diagram to PDF using the configured options
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // This method represents downstream processing that should occur after each page is saved
    static void ProcessSavedPage(int pageIndex, int pageCount)
    {
        // Example downstream logic: log completion and placeholder for further actions
        Console.WriteLine($"Downstream processing for page {pageIndex + 1} of {pageCount} completed.");
        // Additional processing (e.g., extracting page data, updating a database) can be added here
    }

    // Implementation of IPageSavingCallback to receive page save events from PdfSaveOptions
    class MyPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log the start of page saving
            Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log the end of page saving
            Console.WriteLine($"End saving page index {args.PageIndex} of {args.PageCount}");

            // Trigger downstream processing for the saved page
            Program.ProcessSavedPage(args.PageIndex, args.PageCount);
        }
    }
}