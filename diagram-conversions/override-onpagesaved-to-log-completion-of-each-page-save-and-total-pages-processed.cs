using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingLogger : IPageSavingCallback
{
    private int _pagesProcessed = 0;
    private int _totalPages = 0;

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Capture total page count on first callback
        if (_totalPages == 0)
        {
            _totalPages = args.PageCount;
        }
        // Optional: log start of page saving
        Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        _pagesProcessed++;
        Console.WriteLine($"Completed saving page index {args.PageIndex} of {args.PageCount}");
        // No need to modify args.HasMorePages here
    }

    // Expose the processed page count for reporting after the save operation
    public int PagesProcessed => _pagesProcessed;
    public int TotalPages => _totalPages;
}

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options and assign the custom page saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Ensure format tracking is set
            PageSavingLogger logger = new PageSavingLogger();
            pdfOptions.PageSavingCallback = logger;

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

            // After saving, report total pages processed
            Console.WriteLine($"Total pages processed: {logger.PagesProcessed} of {logger.TotalPages}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}