using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingTest
{
    // Custom callback to track page saving events
    public class PageSavingCallback : IPageSavingCallback
    {
        public int StartCount { get; private set; } = 0;
        public int EndCount { get; private set; } = 0;
        public List<int> PageIndices { get; } = new List<int>();

        // Called before a page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            StartCount++;
            PageIndices.Add(args.PageIndex);
        }

        // Called after a page is saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            EndCount++;
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a new diagram and add two pages
            using (Diagram diagram = new Diagram())
            {
                // Ensure at least two pages exist
                diagram.Pages.Add(new Page()); // Page 0 (default may already exist)
                diagram.Pages.Add(new Page()); // Page 1

                int expectedPageCount = diagram.Pages.Count;

                // Set up PDF save options with the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                PageSavingCallback callback = new PageSavingCallback();
                pdfOptions.PageSavingCallback = callback;

                // Save the diagram (output path can be any writable location)
                string outputPath = "multi_page_output.pdf";
                diagram.Save(outputPath, pdfOptions);

                // Verify that the callback was invoked for each page
                if (callback.StartCount != expectedPageCount)
                {
                    throw new Exception($"PageStartSaving was called {callback.StartCount} times, expected {expectedPageCount}.");
                }

                if (callback.EndCount != expectedPageCount)
                {
                    throw new Exception($"PageEndSaving was called {callback.EndCount} times, expected {expectedPageCount}.");
                }

                // Optional: verify that page indices are sequential starting from 0
                for (int i = 0; i < expectedPageCount; i++)
                {
                    if (callback.PageIndices[i] != i)
                    {
                        throw new Exception($"Unexpected page index {callback.PageIndices[i]} at position {i}, expected {i}.");
                    }
                }

                Console.WriteLine("OnPageSaving verification succeeded: callback invoked for each page.");
            }
        }
    }
}