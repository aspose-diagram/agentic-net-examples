using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramConverter <inputPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            Console.WriteLine("Loading diagram...");
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully.");

            // Prepare PDF save options with a page‑saving callback to report progress
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageSavingCallback();

            Console.WriteLine("Starting conversion to PDF...");
            // Save the diagram as PDF using the options that include progress reporting
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Implementation of the page‑saving callback to display real‑time progress
class PageSavingCallback : IPageSavingCallback
{
    // Called before each page is saved
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Saving page {args.PageIndex + 1} of {args.PageCount}...");
    }

    // Called after each page is saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished page {args.PageIndex + 1}.");
        // Example: stop further processing if needed
        // args.HasMorePages = false;
    }
}