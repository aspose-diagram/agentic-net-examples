using System.IO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AutoSpaceWithCancellation
{
    static void Main()
    {
        try
        {

            // Load an existing diagram
            var diagram = new Diagram("input.vsdx");

            // Get the first page (or any specific page)
            var page = diagram.Pages[0];

            // Prepare auto‑spacing options
            var options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // inches
                DistanceInVertical = 0.5    // inches
            };

            // Create a cancellation token source that the user can trigger
            var cts = new CancellationTokenSource();

            // Example: cancel after 2 seconds (replace with real user request)
            Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(_ => cts.Cancel());

            // Run the auto‑spacing operation in a separate task
            var autoSpaceTask = Task.Run(() =>
            {
                // InterruptMonitor will be signaled when cancellation is requested
                var monitor = new InterruptMonitor();

                // Register the token so that when cancellation is requested,
                // the monitor interrupts the long‑running operation.
                cts.Token.Register(() => monitor.Interrupt());

                // Perform auto‑spacing; Aspose.Diagram checks the monitor internally
                // and aborts the operation if interruption is requested.
                page.AutoSpaceShapes(page.Shapes, options);
            }, cts.Token);

            try
            {
                // Wait for the operation to complete or be cancelled
                autoSpaceTask.Wait(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Auto‑spacing was cancelled by the user.");
            }
            catch (AggregateException ae) when (ae.InnerExceptions.Any(e => e is OperationCanceledException))
            {
                Console.WriteLine("Auto‑spacing was cancelled by the user.");
            }

            // Save the diagram (if the operation completed)
            if (!cts.IsCancellationRequested)
            {
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with auto‑spacing applied.");
            }
            else
            {
                Console.WriteLine("Diagram not saved because the operation was cancelled.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
