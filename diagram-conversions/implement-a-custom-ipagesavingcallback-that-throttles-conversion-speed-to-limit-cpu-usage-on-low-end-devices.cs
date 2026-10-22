using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ThrottlingPageSavingCallback : IPageSavingCallback
{
    // Number of milliseconds to pause before processing each page.
    private const int ThrottleDelayMs = 200;

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Log page start information.
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");

        // Introduce a short delay to throttle CPU usage.
        Thread.Sleep(ThrottleDelayMs);
    }

    // Called after a page has been saved.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Log page end information.
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // No further throttling needed here, but could add additional logic if required.
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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Explicitly set the save format.
                SaveFormat = SaveFileFormat.Pdf,
                // Assign the custom throttling callback.
                PageSavingCallback = new ThrottlingPageSavingCallback()
            };

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}