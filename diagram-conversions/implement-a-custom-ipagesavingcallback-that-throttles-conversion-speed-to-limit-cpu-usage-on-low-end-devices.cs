using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Custom callback to throttle page saving during PDF export.
    // Introduces a short delay after each page is saved to reduce CPU usage.
    public class ThrottlingPageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
            // No throttling here; we could add logic if needed.
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            // Introduce a small pause to limit CPU usage on low‑end devices.
            // Adjust the delay (in milliseconds) as appropriate.
            Thread.Sleep(100);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths.
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                // Load the Visio diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Set a default font to avoid missing‑font issues.
                        DefaultFont = "Arial",
                        // Assign the custom throttling callback.
                        PageSavingCallback = new ThrottlingPageSavingCallback()
                    };

                    // Save the diagram as PDF with the throttling callback.
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine("Diagram saved to PDF successfully.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}