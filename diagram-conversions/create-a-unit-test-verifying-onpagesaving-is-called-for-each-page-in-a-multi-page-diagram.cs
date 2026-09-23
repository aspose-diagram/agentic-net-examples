using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingTest
{
    // Callback implementation to track page saving events
    public class PageSavingCounter : IPageSavingCallback
    {
        public int StartCount { get; private set; }
        public int EndCount { get; private set; }
        public List<int> StartedPages { get; } = new List<int>();
        public List<int> EndedPages { get; } = new List<int>();

        public void PageStartSaving(PageStartSavingArgs args)
        {
            StartCount++;
            StartedPages.Add(args.PageIndex);
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            EndCount++;
            EndedPages.Add(args.PageIndex);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a new diagram with two pages
            Diagram diagram = new Diagram();
            // The diagram starts with one page; add a second page
            diagram.Pages.Add(new Page());

            // Verify page count
            int pageCount = diagram.Pages.Count;
            if (pageCount != 2)
                throw new Exception($"Expected 2 pages, but found {pageCount}.");

            // Set up PDF save options with the custom callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            PageSavingCounter callback = new PageSavingCounter();
            pdfOptions.PageSavingCallback = callback;

            // Save the diagram to PDF (output file path can be any valid location)
            string outputPath = "multi_page_output.pdf";
            diagram.Save(outputPath, pdfOptions);

            // Validate that the callback was invoked for each page
            if (callback.StartCount != pageCount)
                throw new Exception($"PageStartSaving was called {callback.StartCount} times; expected {pageCount}.");

            if (callback.EndCount != pageCount)
                throw new Exception($"PageEndSaving was called {callback.EndCount} times; expected {pageCount}.");

            // Optional: verify that page indices reported match expected range
            for (int i = 0; i < pageCount; i++)
            {
                if (!callback.StartedPages.Contains(i))
                    throw new Exception($"PageStartSaving did not report page index {i}.");
                if (!callback.EndedPages.Contains(i))
                    throw new Exception($"PageEndSaving did not report page index {i}.");
            }

            Console.WriteLine("All page saving callbacks were invoked correctly for each page.");
        }
    }
}