using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingDemo
{
    // Custom callback to receive page saving events.
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Called when a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log the start of the page save operation.
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
            // Optionally, you can control whether the page should be output.
            // args.IsToOutput = true;
        }

        // Called when a page finishes saving.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No action needed for this example.
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options and attach the custom callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new MyPageSavingCallback()
                };

                // Save the diagram to PDF; the callback will log each page start.
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}