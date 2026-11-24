using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an InterruptMonitor to allow aborting long‑running operations
        InterruptMonitor monitor = new InterruptMonitor();

        // Start a background thread that will request an interrupt after a timeout (e.g., 5 seconds)
        Thread interruptThread = new Thread(() =>
        {
            // Wait for the defined timeout period
            Thread.Sleep(5000);
            // Signal the monitor to interrupt the ongoing operation
            monitor.Interrupt();
            Console.WriteLine("Interrupt requested.");
        });
        interruptThread.Start();

        try
        {
            // Configure LoadOptions with the interrupt monitor attached
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
            loadOptions.InterruptMonitor = monitor;

            // Load the diagram while monitoring for possible interruption
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Assign the same monitor to the diagram for post‑load operations (e.g., saving)
            diagram.InterruptMonitor = monitor;

            // Prepare PDF save options (default settings)
            PdfSaveOptions saveOptions = new PdfSaveOptions();

            // Save the diagram to PDF; this operation can also be interrupted
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Log that the operation was aborted due to an interrupt or other error
            Console.Error.WriteLine($"Operation aborted: {ex.Message}");
        }
        finally
        {
            // Ensure the interrupt thread has completed before exiting
            interruptThread.Join();
        }
    }
}