using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output PDF path (same folder, same name with .pdf extension)
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Capture the expected page count from the loaded diagram
                int expectedPageCount = diagram.Pages.Count;

                // Create PDF save options and assign the custom page-saving callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PageCountValidatorCallback(expectedPageCount);

                // Save the diagram to PDF; the callback will be invoked for each page
                diagram.Save(outputPath, pdfOptions);

                // If we reach this point, the callback validation succeeded
                Console.WriteLine($"Diagram saved to '{outputPath}'. Page count validation succeeded (Pages = {expectedPageCount}).");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom callback implementation to validate page count during PDF saving
class PageCountValidatorCallback : IPageSavingCallback
{
    private readonly int _expectedPageCount;

    public PageCountValidatorCallback(int expectedPageCount)
    {
        _expectedPageCount = expectedPageCount;
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Validate that the total page count reported by the callback matches the expected count
        if (args.PageCount != _expectedPageCount)
        {
            Console.Error.WriteLine($"PageStartSaving: Callback reported page count {args.PageCount}, but expected {_expectedPageCount}.");
            throw new Exception("Page count mismatch detected in PageStartSaving.");
        }
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Perform the same validation after the page is saved
        if (args.PageCount != _expectedPageCount)
        {
            Console.Error.WriteLine($"PageEndSaving: Callback reported page count {args.PageCount}, but expected {_expectedPageCount}.");
            throw new Exception("Page count mismatch detected in PageEndSaving.");
        }
    }
}