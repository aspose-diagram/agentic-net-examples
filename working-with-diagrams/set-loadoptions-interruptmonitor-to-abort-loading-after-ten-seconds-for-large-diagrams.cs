using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Create LoadOptions and assign an InterruptMonitor.
        LoadOptions loadOptions = new LoadOptions();
        InterruptMonitor monitor = new InterruptMonitor();
        loadOptions.InterruptMonitor = monitor;

        // Start a background thread that will interrupt loading after 10 seconds.
        Thread interruptThread = new Thread(() =>
        {
            // Wait for 10,000 milliseconds (10 seconds).
            Thread.Sleep(10000);
            // Trigger the interrupt.
            monitor.Interrupt();
        });
        interruptThread.IsBackground = true;
        interruptThread.Start();

        try
        {
            // Load the diagram with the configured LoadOptions.
            using (Diagram diagram = new Diagram(inputPath, loadOptions))
            {
                // If loading succeeds, optionally perform further processing.
                Console.WriteLine("Diagram loaded successfully.");
                // Example: save the diagram to verify it was loaded.
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture and report any errors, including those caused by interruption.
            Console.Error.WriteLine($"Error during diagram loading or processing: {ex.Message}");
        }
        finally
        {
            // Ensure the interrupt thread is terminated if still running.
            if (interruptThread.IsAlive)
            {
                try
                {
                    interruptThread.Abort();
                }
                catch
                {
                    // Ignored: thread abort may not be supported; rely on process exit.
                }
            }
        }
    }
}