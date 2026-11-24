using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an InterruptMonitor to allow aborting the load operation
        InterruptMonitor monitor = new InterruptMonitor();

        // Configure LoadOptions with the monitor
        LoadOptions loadOptions = new LoadOptions();
        loadOptions.InterruptMonitor = monitor;

        // Start a background thread that will interrupt after 10 seconds
        Thread interruptThread = new Thread(() =>
        {
            // Wait for 10,000 milliseconds (10 seconds)
            Thread.Sleep(10000);
            // Signal the monitor to abort the loading process
            monitor.Interrupt();
        });
        interruptThread.Start();

        try
        {
            // Load the diagram using the configured LoadOptions inside a using block
            using (Diagram diagram = new Diagram(inputPath, loadOptions))
            {
                // Save the diagram to verify successful load (if not interrupted)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram loaded and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture and report any errors, including interruption
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
        }
        finally
        {
            // Ensure the interrupt thread finishes before exiting
            interruptThread.Join();
        }
    }
}