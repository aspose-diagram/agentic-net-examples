using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingTest
{
    // Custom callback to count page saving events
    public class PageSavingCounter : IPageSavingCallback
    {
        public int StartCount { get; private set; } = 0;

        // Called before each page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            StartCount++;
            Console.WriteLine($"Starting save of page {args.PageIndex + 1} of {args.PageCount}");
        }

        // Called after each page is saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No action needed for this test
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a new diagram
            Diagram diagram = new Diagram();

            // Ensure the diagram has multiple pages (e.g., 3 pages)
            // The default diagram may already contain one page; add additional pages as needed
            while (diagram.Pages.Count < 3)
            {
                // Add a new page with a unique ID
                int newId = diagram.Pages.Count + 1;
                diagram.Pages.Add(new Page(newId));
            }

            int expectedPageCount = diagram.Pages.Count;

            // Set up PDF save options with the custom page-saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            PageSavingCounter callback = new PageSavingCounter();
            pdfOptions.PageSavingCallback = callback;

            // Define output path (in the current directory)
            string outputPath = "MultiPageDiagram.pdf";

            // Save the diagram as PDF; this will trigger the callback for each page
            diagram.Save(outputPath, pdfOptions);

            // Verify that the callback was invoked for each page
            if (callback.StartCount != expectedPageCount)
            {
                throw new Exception($"Page saving callback was called {callback.StartCount} times, expected {expectedPageCount} times.");
            }

            Console.WriteLine("Test passed: OnPageSaving (PageStartSaving) was called for each page.");
        }
    }
}