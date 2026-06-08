using System.IO;
using System;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the VSD file to be loaded.
        string inputPath = "sample.vsd";

        // Create an InterruptMonitor that can be used to cancel the load operation.
        InterruptMonitor monitor = new InterruptMonitor();

        // Example: start a background thread that will request interruption after a delay.
        Thread interrupter = new Thread(() =>
        {
            Thread.Sleep(2000); // Wait 2 seconds.
            monitor.Interrupt();
            Console.WriteLine("Interrupt requested.");
        });
        interrupter.Start();

        // Configure LoadOptions with the desired format and attach the monitor.
        LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsd);
        loadOptions.InterruptMonitor = monitor;

        try
        {
            // Load the diagram using the custom LoadOptions.
            Diagram diagram = new Diagram(inputPath, loadOptions);
            Console.WriteLine("Diagram loaded successfully.");
            // Diagram processing can be performed here.
        }
        catch (Exception ex)
        {
            // Loading was interrupted or failed.
            Console.WriteLine($"Loading failed or was interrupted: {ex.Message}");
        }

        // Ensure the interrupter thread has completed before exiting.
        interrupter.Join();
    }
}
