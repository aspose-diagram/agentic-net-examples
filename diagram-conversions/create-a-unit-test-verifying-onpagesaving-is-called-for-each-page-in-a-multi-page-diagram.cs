using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingCounter : IPageSavingCallback
{
    // Counts how many times each callback method is invoked.
    public int StartCount { get; private set; }
    public int EndCount { get; private set; }

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Increment the start counter.
        StartCount++;
        // Example: you could log args.PageIndex here if needed.
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Increment the end counter.
        EndCount++;
        // Example: you could log args.PageIndex here if needed.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Path to the multi‑page Visio file to test.
        string inputPath = "sample_multi_page.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Path for the generated PDF output.
        string outputPath = "output.pdf";

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Determine the expected number of pages.
            int expectedPageCount = diagram.Pages.Count;

            // Create the callback instance that will count page events.
            PageSavingCounter callback = new PageSavingCounter();

            // Configure PDF save options and assign the callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = callback;

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);

            // Verify that the callback was invoked for each page.
            if (callback.StartCount != expectedPageCount)
            {
                throw new Exception($"PageStartSaving was called {callback.StartCount} times, expected {expectedPageCount}.");
            }

            if (callback.EndCount != expectedPageCount)
            {
                throw new Exception($"PageEndSaving was called {callback.EndCount} times, expected {expectedPageCount}.");
            }

            // If we reach this point, the test succeeded.
            Console.WriteLine("Success: OnPageSaving (PageStartSaving/PageEndSaving) was called for each page.");

            // Clean up resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}