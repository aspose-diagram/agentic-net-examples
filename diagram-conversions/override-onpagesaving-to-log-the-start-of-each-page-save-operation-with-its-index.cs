using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageSavingExample
{
    // Custom callback to log the start of each page saving operation.
    public class CustomPageSavingCallback : IPageSavingCallback
    {
        // Called before a page is saved.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log page index (zero‑based) and total page count.
            Console.WriteLine($"Starting save for page {args.PageIndex + 1} of {args.PageCount}");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No additional actions needed after page save.
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace "input.vsdx" with the actual path to your diagram file.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options and assign the custom page‑saving callback.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

                // Save the diagram as PDF.
                // Replace "output.pdf" with the desired output file path.
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}