using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionThrottle
{
    // Custom callback to throttle PDF page rendering speed.
    // Introduces a minimum delay per page to reduce CPU usage on low‑end devices.
    public class ThrottlingCallback : IPageSavingCallback
    {
        // Minimum time (in milliseconds) each page should take to render.
        private const int MinPageRenderTimeMs = 200;

        // Timestamp when the current page started rendering.
        private DateTime _pageStartTime;

        // Called before a page starts saving.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Record the start time for this page.
            _pageStartTime = DateTime.UtcNow;
            // Optional: log page start.
            Console.WriteLine($"Starting save of page {args.PageIndex + 1} of {args.PageCount}.");
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Calculate elapsed time.
            var elapsedMs = (int)(DateTime.UtcNow - _pageStartTime).TotalMilliseconds;

            // If rendering was faster than the minimum, pause to throttle CPU usage.
            if (elapsedMs < MinPageRenderTimeMs)
            {
                int delay = MinPageRenderTimeMs - elapsedMs;
                Console.WriteLine($"Throttling: delaying {delay} ms for page {args.PageIndex + 1}.");
                Thread.Sleep(delay);
            }

            // Optionally stop further processing (not used here).
            // args.HasMorePages = false;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                string inputPath = "input.vsdx";

                // Path for the output PDF.
                string outputPath = "output.pdf";

                // Load the diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Set a fallback font to avoid missing‑font warnings.
                        DefaultFont = "Arial"
                    };

                    // Assign the throttling callback.
                    pdfOptions.PageSavingCallback = new ThrottlingCallback();

                    // Save the diagram as PDF with the throttling behavior.
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Conversion completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}