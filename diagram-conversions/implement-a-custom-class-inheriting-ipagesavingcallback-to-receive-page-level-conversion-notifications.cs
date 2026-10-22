using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log start of page saving.
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log completion of page saving.
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: stop further processing after the first page.
        // Uncomment the following line to stop after the first page.
        // args.HasMorePages = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path.
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Assign the custom page-saving callback.
            pdfOptions.PageSavingCallback = new PageSavingCallback();

            // Save the diagram to PDF using the options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}