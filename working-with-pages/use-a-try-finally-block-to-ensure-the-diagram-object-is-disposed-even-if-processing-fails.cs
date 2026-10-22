using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF file path
        string outputPath = "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram
            diagram = new Diagram(inputPath);

            // Create PDF save options and assign a custom page-saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new CustomPdfCallback();

            // Save the diagram as PDF using the options
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram successfully saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Diagram does not implement IDisposable; setting to null allows GC to reclaim it
            diagram = null;
        }
    }
}

// Custom callback to handle PDF page saving events
class CustomPdfCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
        // Example: you could modify args here if needed
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
        // If you need to stop further processing, set HasMorePages to FALSE
        // args.HasMorePages = BOOL.False; // Uncomment to stop after the first page
    }
}