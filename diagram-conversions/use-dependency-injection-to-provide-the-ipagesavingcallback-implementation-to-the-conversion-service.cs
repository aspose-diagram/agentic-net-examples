using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        // No modification of args needed here.
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        // Example: stop after the first page (uncomment to use)
        // if (args.PageIndex == 0)
        // {
        //     args.HasMorePages = false; // Use plain bool as required by the API.
        // }
    }
}

// Simple conversion service that uses constructor injection for the callback.
class DiagramConversionService
{
    private readonly IPageSavingCallback _pageSavingCallback;

    public DiagramConversionService(IPageSavingCallback pageSavingCallback)
    {
        _pageSavingCallback = pageSavingCallback ?? throw new ArgumentNullException(nameof(pageSavingCallback));
    }

    // Converts a Visio diagram to PDF using the injected callback.
    public void ConvertToPdf(string inputPath, string outputPath)
    {
        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Example setting: do not export hidden pages.
                    ExportHiddenPage = false, // Changed from BOOL.False to plain bool.
                    // Assign the injected callback.
                    PageSavingCallback = _pageSavingCallback
                };

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully converted to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output paths.
        string inputPath;
        string outputPath;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            outputPath = args[1];
        }
        else
        {
            // Default paths (adjust as needed).
            inputPath = "input.vsdx";
            outputPath = "output.pdf";
        }

        // Guard against missing input file before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Set up dependency injection manually.
        IPageSavingCallback callback = new CustomPageSavingCallback();
        DiagramConversionService conversionService = new DiagramConversionService(callback);

        // Perform the conversion.
        conversionService.ConvertToPdf(inputPath, outputPath);
    }
}
