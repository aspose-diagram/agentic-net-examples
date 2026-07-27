using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingDemo
{
    // Custom callback that receives page‑saving events from Aspose.Diagram
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Called before a page is written to the output file
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Example: you could cancel saving of a specific page
            // args.IsToOutput = false; // skip page
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
        }

        // Called after a page has been written to the output file
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");

            // Trigger downstream processing for the just‑saved page
            // In a real scenario you might locate the generated page file,
            // move it, upload it, or invoke another service.
            ProcessSavedPage(args.PageIndex);
            
            // Indicate whether more pages will follow (default is true)
            // args.HasMorePages = args.PageIndex < args.PageCount - 1;
        }

        // Example downstream processing method
        private void ProcessSavedPage(int pageIndex)
        {
            // Placeholder for custom logic – e.g., logging, file handling, etc.
            Console.WriteLine($"[Downstream] Processing saved page #{pageIndex + 1}");
            // Add your code here.
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options and attach the callback
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new MyPageSavingCallback()
                };

                // Save the diagram to PDF; the callback will be invoked per page
                diagram.Save("output.pdf", saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}