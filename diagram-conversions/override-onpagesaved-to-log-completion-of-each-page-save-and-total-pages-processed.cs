using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingDemo
{
    // Custom callback to monitor page saving progress
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Called when a page starts saving (optional implementation)
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // You can log start of page saving here if needed
            // Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
        }

        // Called when a page finishes saving
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log completion of the current page
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");

            // Update HasMorePages if you want to stop early; default is true
            // args.HasMorePages = true;
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
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new MyPageSavingCallback()
                };

                // Save the diagram as PDF; the callback will log each page saved
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}