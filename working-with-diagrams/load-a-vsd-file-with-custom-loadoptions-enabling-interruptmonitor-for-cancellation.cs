using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path (use first argument or a default placeholder)
        string inputPath = args.Length > 0 ? args[0] : "input.vsd";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
            return;
        }

        // Create an InterruptMonitor instance that can be used to cancel the load operation
        InterruptMonitor interruptMonitor = new InterruptMonitor();

        // Optionally start a background thread that will interrupt after a timeout (e.g., 5 seconds)
        Thread monitorThread = new Thread(() =>
        {
            // Wait for 5 seconds before triggering interruption
            Thread.Sleep(5000);
            // Request cancellation
            interruptMonitor.Interrupt();
            Console.WriteLine("Interrupt requested by monitor thread.");
        });
        monitorThread.Start();

        try
        {
            // Configure LoadOptions with the desired file format and attach the InterruptMonitor
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsd);
            loadOptions.InterruptMonitor = interruptMonitor;

            // Load the Visio diagram using the custom LoadOptions
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // If loading succeeds, indicate success
            Console.WriteLine("Diagram loaded successfully.");
        }
        catch (Exception ex)
        {
            // Capture and display any errors, including those caused by interruption
            Console.Error.WriteLine($"An error occurred while loading the diagram: {ex.Message}");
        }
        finally
        {
            // Ensure the monitor thread finishes before exiting
            monitorThread.Join();
        }
    }
}