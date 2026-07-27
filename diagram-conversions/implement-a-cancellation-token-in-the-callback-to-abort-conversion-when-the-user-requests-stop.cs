using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Token source that can be triggered by the user (e.g., UI button)
            var cts = new CancellationTokenSource();

            // Example: automatically cancel after 5 seconds (replace with real user action)
            var timer = new Timer(_ => cts.Cancel(), null, 5000, Timeout.Infinite);

            // Create an Aspose interrupt monitor
            var interruptMonitor = new InterruptMonitor();

            // When the token is cancelled, signal the monitor to interrupt the operation
            cts.Token.Register(() => interruptMonitor.Interrupt());

            // Load options with the interrupt monitor attached
            var loadOptions = new LoadOptions
            {
                InterruptMonitor = interruptMonitor
            };

            // Load the diagram (potentially long‑running)
            Diagram diagram = new Diagram("input.vsdx", loadOptions);

            // Save options with a page‑saving callback that also checks cancellation
            var saveOptions = new PdfSaveOptions
            {
                PageSavingCallback = new CancelPageSavingCallback(cts.Token)
            };

            // Save the diagram (potentially long‑running)
            diagram.Save("output.pdf", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Callback that aborts the save operation when cancellation is requested
    class CancelPageSavingCallback : IPageSavingCallback
    {
        private readonly CancellationToken _token;

        public CancelPageSavingCallback(CancellationToken token)
        {
            _token = token;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No special handling needed at the start of a page
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // If cancellation was requested, throw to stop the save process
            if (_token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Conversion cancelled by user.");
            }
        }
    }
}