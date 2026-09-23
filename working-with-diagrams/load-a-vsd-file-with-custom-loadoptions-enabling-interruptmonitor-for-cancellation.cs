using System.IO;
using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the Visio file to load
        string inputPath = "input.vsdx";

        // Create an InterruptMonitor to allow cancellation
        InterruptMonitor monitor = new InterruptMonitor();

        // Optional: start a background thread that will interrupt after a delay
        Thread interrupter = new Thread(() =>
        {
            Thread.Sleep(2000); // wait 2 seconds before interrupting
            monitor.Interrupt();
            Console.WriteLine("Loading operation was interrupted.");
        });
        interrupter.Start();

        // Configure LoadOptions with the interrupt monitor
        LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
        loadOptions.InterruptMonitor = monitor;

        try
        {
            // Load the diagram using the custom LoadOptions
            Diagram diagram = new Diagram(inputPath, loadOptions);
            Console.WriteLine("Diagram loaded successfully.");
            // Additional processing can be performed here
        }
        catch (Exception ex)
        {
            // Handle exceptions, including those caused by interruption
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
        }

        // Ensure the interrupter thread has finished
        interrupter.Join();
    }
}
