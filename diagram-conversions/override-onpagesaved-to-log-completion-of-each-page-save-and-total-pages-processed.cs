using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Do not export hidden pages. (ExportHiddenPage expects a bool, not BOOL.)
                pdfOptions.ExportHiddenPage = false;

                // Assign the custom page-saving callback to log progress.
                CustomPageSavingCallback callback = new CustomPageSavingCallback();
                pdfOptions.PageSavingCallback = callback;

                // Save the diagram to PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);

                // After saving, report total pages processed.
                Console.WriteLine($"Total pages processed: {callback.TotalPages}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Custom implementation of IPageSavingCallback to log page save progress.
class CustomPageSavingCallback : IPageSavingCallback
{
    // Counter for the number of pages that have completed saving.
    public int TotalPages { get; private set; } = 0;

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log start of page saving.
        Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log completion of page saving.
        Console.WriteLine($"End saving page index {args.PageIndex} of {args.PageCount}");

        // Increment the total pages counter.
        TotalPages++;
    }
}
