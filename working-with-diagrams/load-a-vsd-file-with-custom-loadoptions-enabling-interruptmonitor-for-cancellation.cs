using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the VSD file to be loaded
            string inputPath = "sample.vsd";

            // Create an InterruptMonitor to enable cancellation of the load operation
            InterruptMonitor monitor = new InterruptMonitor();

            // Configure LoadOptions with the desired format and assign the monitor
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsd);
            loadOptions.InterruptMonitor = monitor;

            // Optional: start a background thread that triggers cancellation after a delay
            System.Threading.Thread cancelThread = new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(2000); // wait 2 seconds
                monitor.Interrupt(); // request interruption
            });
            cancelThread.Start();

            try
            {
                // Load the diagram using the custom LoadOptions
                Diagram diagram = new Diagram(inputPath, loadOptions);
                Console.WriteLine("Diagram loaded successfully.");
                // Diagram processing logic can be placed here
            }
            catch (Exception ex)
            {
                // Handle interruption or other loading errors
                Console.WriteLine($"Loading was interrupted or failed: {ex.Message}");
            }

            // Ensure the cancellation thread has completed before exiting
            cancelThread.Join();
        }
    }