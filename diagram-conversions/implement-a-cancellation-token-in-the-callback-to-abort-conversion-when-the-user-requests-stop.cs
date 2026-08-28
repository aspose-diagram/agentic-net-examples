using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Callback that checks a CancellationToken and requests interruption if cancellation is requested.
    class PageSavingCallback : IPageSavingCallback
    {
        private readonly CancellationToken _cancellationToken;
        private readonly InterruptMonitor _interruptMonitor;

        public PageSavingCallback(CancellationToken cancellationToken, InterruptMonitor interruptMonitor)
        {
            _cancellationToken = cancellationToken;
            _interruptMonitor = interruptMonitor;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start; interruption is checked on each page end.
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // If the user has requested cancellation, signal Aspose.Diagram to abort.
            if (_cancellationToken.IsCancellationRequested)
            {
                _interruptMonitor.Interrupt();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a cancellation token source that can be triggered by the user.
                var cts = new CancellationTokenSource();

                // Example: cancel after 5 seconds (replace with real user interaction).
                Timer timer = new Timer(_ => cts.Cancel(), null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);

                // Set up the interrupt monitor and associate it with load options.
                var interruptMonitor = new InterruptMonitor();
                var loadOptions = new LoadOptions
                {
                    InterruptMonitor = interruptMonitor
                };

                // Load the diagram using the interrupt monitor.
                var diagram = new Diagram("input.vsdx", loadOptions);

                // Configure PDF save options and attach the page‑saving callback.
                var pdfOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new PageSavingCallback(cts.Token, interruptMonitor)
                };

                // Perform the conversion; the operation will be aborted if cancellation is requested.
                diagram.Save("output.pdf", pdfOptions);

                // Clean up timer.
                timer.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}