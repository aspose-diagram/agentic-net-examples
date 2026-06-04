using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Custom callback that ensures each page takes at least a minimum amount of time.
    // This throttles the conversion speed, reducing CPU spikes on low‑end devices.
    public class ThrottlingPageSavingCallback : IPageSavingCallback
    {
        // Minimum time a page should occupy during saving.
        private readonly TimeSpan _minPageTime;

        // Stores the start time for each page (keyed by page index).
        private readonly ConcurrentDictionary<int, Stopwatch> _pageTimers = new ConcurrentDictionary<int, Stopwatch>();

        public ThrottlingPageSavingCallback(TimeSpan minPageTime)
        {
            _minPageTime = minPageTime;
        }

        // Called just before a page starts to be saved.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Record the start time for the current page.
            var sw = Stopwatch.StartNew();
            _pageTimers[args.PageIndex] = sw;

            // Optionally you could decide to skip pages on very low‑end devices:
            // args.IsToOutput = true; // keep default behavior
        }

        // Called after a page has been saved.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Retrieve the stopwatch for this page.
            if (_pageTimers.TryRemove(args.PageIndex, out var sw))
            {
                sw.Stop();
                var elapsed = sw.Elapsed;

                // If the page finished faster than the minimum time, pause the thread.
                if (elapsed < _minPageTime)
                {
                    var remaining = _minPageTime - elapsed;
                    Thread.Sleep(remaining);
                }
            }

            // Indicate whether more pages remain; default is true.
            args.HasMorePages = args.PageIndex < args.PageCount - 1;
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // (Replace "input.vsdx" with the actual file path.)
                Diagram diagram = new Diagram("input.vsdx");

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Attach the throttling callback.
                    PageSavingCallback = new ThrottlingPageSavingCallback(TimeSpan.FromMilliseconds(200))
                };

                // Save the diagram as PDF.
                // (Replace "output.pdf" with the desired output path.)
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}