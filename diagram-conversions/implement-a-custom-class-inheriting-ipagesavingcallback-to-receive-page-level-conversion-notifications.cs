using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingDemo
{
    // Custom callback to receive page‑level notifications during saving
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Called when a page starts to be saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Example: write start info to console
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");

            // Optionally cancel saving of a specific page
            // args.IsToOutput = false; // uncomment to skip a page
        }

        // Called when a page finishes saving
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Example: write end info to console
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Indicate whether more pages remain (default is true)
            // args.HasMorePages = args.PageIndex < args.PageCount - 1;
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

                // Configure PDF save options and attach the custom callback
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new MyPageSavingCallback()
                };

                // Save the diagram to PDF; callbacks will be invoked for each page
                diagram.Save("output.pdf", saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}