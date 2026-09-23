using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageCallbackExample
{
    // Custom callback to receive page‑level notifications during PDF export
    public class MyPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

            // Example: stop processing further pages (uncomment to use)
            // args.HasMorePages = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options and assign the custom callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new MyPageSavingCallback();

                // Export the diagram to PDF with page‑level notifications
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram has been saved to PDF.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}