using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageTimingExample
{
    // Custom callback to capture timestamps for each page during PDF saving
    public class TimingPageSavingCallback : IPageSavingCallback
    {
        // Stores the start time for each page index
        private readonly Dictionary<int, Stopwatch> _stopwatches = new Dictionary<int, Stopwatch>();

        // Stores the elapsed time for each page after it is saved
        public readonly List<TimeSpan> PageDurations = new List<TimeSpan>();

        // Called when a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Ensure we have a stopwatch for the current page
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            _stopwatches[args.PageIndex] = stopwatch;
        }

        // Called when a page finishes saving
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Retrieve and stop the stopwatch for the current page
            if (_stopwatches.TryGetValue(args.PageIndex, out var stopwatch))
            {
                stopwatch.Stop();
                PageDurations.Add(stopwatch.Elapsed);
                _stopwatches.Remove(args.PageIndex);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                var diagram = new Diagram(@"InputDiagram.vsdx");

                // Create PDF save options and attach the custom page saving callback
                var pdfOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new TimingPageSavingCallback()
                };

                // Save the diagram as PDF; the callback will be invoked for each page
                diagram.Save(@"OutputDiagram.pdf", pdfOptions);

                // After saving, retrieve the callback to calculate average processing time
                var timingCallback = (TimingPageSavingCallback)pdfOptions.PageSavingCallback;

                // Calculate average duration per page
                if (timingCallback.PageDurations.Count > 0)
                {
                    var averageTicks = timingCallback.PageDurations.Average(ts => ts.Ticks);
                    var averageTime = new TimeSpan(Convert.ToInt64(averageTicks));

                    Console.WriteLine($"Processed {timingCallback.PageDurations.Count} pages.");
                    Console.WriteLine($"Average page processing time: {averageTime.TotalMilliseconds} ms");
                }
                else
                {
                    Console.WriteLine("No page timing data was captured.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}