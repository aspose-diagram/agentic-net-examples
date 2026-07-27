using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingTest
{
    // Custom callback to track page saving events
    public class TestPageSavingCallback : IPageSavingCallback
    {
        // Counter for the number of pages started to save
        public int PagesStarted { get; private set; } = 0;

        // Counter for the number of pages finished saving
        public int PagesEnded { get; private set; } = 0;

        // Called before a page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            PagesStarted++;
            Console.WriteLine($"PageStartSaving: Index={args.PageIndex}, Count={args.PageCount}");
        }

        // Called after a page is saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            PagesEnded++;
            Console.WriteLine($"PageEndSaving: Index={args.PageIndex}, Count={args.PageCount}");
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a new diagram with two pages
            Diagram diagram = new Diagram();
            // First page is created by default; add a second page
            diagram.Pages.Add(new Page());

            // Verify the diagram has the expected number of pages
            int expectedPageCount = diagram.Pages.Count;
            Console.WriteLine($"Diagram contains {expectedPageCount} page(s).");

            // Prepare PDF save options and assign the custom callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            TestPageSavingCallback callback = new TestPageSavingCallback();
            pdfOptions.PageSavingCallback = callback;

            // Save the diagram to PDF (output path can be adjusted as needed)
            string outputPath = "MultiPageDiagram.pdf";
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

            // Validate that the callback was invoked for each page
            if (callback.PagesStarted != expectedPageCount)
            {
                throw new Exception($"PageStartSaving was called {callback.PagesStarted} times, expected {expectedPageCount}.");
            }

            if (callback.PagesEnded != expectedPageCount)
            {
                throw new Exception($"PageEndSaving was called {callback.PagesEnded} times, expected {expectedPageCount}.");
            }

            Console.WriteLine("All page saving callbacks were invoked correctly.");
        }
    }
}