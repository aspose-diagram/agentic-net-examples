using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the page-saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new RetryPageSavingCallback();

            // Retry mechanism parameters
            const int maxRetries = 3;
            int attempt = 0;
            bool saved = false;

            // Attempt to save the diagram with retries
            while (attempt < maxRetries && !saved)
            {
                try
                {
                    attempt++;
                    diagram.Save(outputPath, pdfOptions);
                    saved = true;
                    Console.WriteLine($"Diagram saved successfully on attempt {attempt}.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Save attempt {attempt} failed: {ex.Message}");
                    if (attempt >= maxRetries)
                    {
                        Console.Error.WriteLine("All retry attempts have been exhausted.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors during loading or processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Custom callback to track page saving progress
class RetryPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"End saving page index {args.PageIndex} of {args.PageCount}");
        // No explicit failure flag is provided by the API.
        // If needed, additional logic could be added here to set args.HasMorePages = BOOL.False;
    }
}