using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageSavingCounter : IPageSavingCallback
{
    public int StartCount { get; private set; }
    public int EndCount { get; private set; }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        StartCount++;
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        EndCount++;
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to a multi‑page Visio file
        string diagramPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram has more than one page for the test
                int pageCount = diagram.Pages.Count;
                if (pageCount < 1)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Prepare PDF save options with the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                PageSavingCounter callback = new PageSavingCounter();
                pdfOptions.PageSavingCallback = callback;

                // Define output PDF path
                string outputPdf = Path.ChangeExtension(diagramPath, ".pdf");

                // Save the diagram as PDF using the options
                diagram.Save(outputPdf, pdfOptions);

                // Verify that the callback was invoked for each page
                if (callback.StartCount != pageCount)
                {
                    throw new Exception($"PageStartSaving was called {callback.StartCount} times, expected {pageCount}.");
                }

                if (callback.EndCount != pageCount)
                {
                    throw new Exception($"PageEndSaving was called {callback.EndCount} times, expected {pageCount}.");
                }

                Console.WriteLine("Success: PageSavingCallback was invoked for each page.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}