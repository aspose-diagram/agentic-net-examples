using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define output PDF path (same folder, same name with .pdf extension)
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create PDF save options and assign the custom page-saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            MyPageSavingCallback callback = new MyPageSavingCallback();
            pdfOptions.PageSavingCallback = callback;

            // Save the diagram to PDF using the options
            diagram.Save(outputPath, pdfOptions);

            // After saving, compare the page count reported by the callback with the diagram's page collection size
            int reportedPageCount = callback.ReportedPageCount;
            int actualPageCount = diagram.Pages.Count;

            if (reportedPageCount != actualPageCount)
            {
                throw new Exception($"Page count mismatch: Callback reported {reportedPageCount}, but diagram contains {actualPageCount} pages.");
            }
            else
            {
                Console.WriteLine($"Page count validation succeeded. Total pages: {actualPageCount}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Custom callback implementation to capture page count during PDF export
class MyPageSavingCallback : IPageSavingCallback
{
    // Stores the total page count reported by the first callback invocation
    public int ReportedPageCount { get; private set; } = -1;
    private bool _isFirstCall = true;

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Capture the total page count only once (it is the same for all pages)
        if (_isFirstCall)
        {
            ReportedPageCount = args.PageCount;
            _isFirstCall = false;
        }
        // Additional per-page logic can be added here if needed
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // No additional actions required for this validation
    }
}