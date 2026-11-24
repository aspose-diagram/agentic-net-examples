using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the VSD file as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create an InterruptMonitor to allow cancellation of the load operation
        InterruptMonitor monitor = new InterruptMonitor();

        // Optional: start a background thread that will interrupt the load after a timeout
        Thread interruptThread = new Thread(() =>
        {
            // Wait for 5 seconds before triggering interruption
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("Interrupting the load operation...");
            monitor.Interrupt();
        });

        interruptThread.Start();

        try
        {
            // Configure LoadOptions with the desired file format and attach the monitor
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsd);
            loadOptions.InterruptMonitor = monitor;

            // Load the diagram using the custom LoadOptions
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // If loading succeeds, you can proceed with further processing
            Console.WriteLine("Diagram loaded successfully.");
        }
        catch (Exception ex)
        {
            // Capture any exceptions, including those thrown due to interruption
            Console.Error.WriteLine($"An error occurred while loading the diagram: {ex.Message}");
        }
        finally
        {
            // Ensure the interrupt thread has finished before exiting
            interruptThread.Join();
        }
    }
}