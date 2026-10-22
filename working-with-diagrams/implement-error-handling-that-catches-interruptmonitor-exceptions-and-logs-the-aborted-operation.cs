using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Create an InterruptMonitor to allow aborting long operations
        InterruptMonitor monitor = new InterruptMonitor();

        // Prepare load options and attach the monitor
        LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
        loadOptions.InterruptMonitor = monitor;

        // Start a background thread that will interrupt after a short delay
        Thread interrupter = new Thread(() =>
        {
            // Wait for 2 seconds before triggering interruption
            Thread.Sleep(2000);
            monitor.Interrupt();
        });
        interrupter.Start();

        try
        {
            // Attempt to load the diagram with monitoring enabled
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // If loading succeeds, optionally save to PDF to demonstrate further processing
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.pdf");
            diagram.Save(outputPath, SaveFileFormat.Pdf);
            Console.WriteLine($"Diagram successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any exception, specifically handling interruption scenarios
            Console.Error.WriteLine($"Operation aborted: {ex.Message}");
        }
        finally
        {
            // Ensure the interrupter thread has finished
            if (interrupter.IsAlive)
            {
                interrupter.Join();
            }
        }
    }
}