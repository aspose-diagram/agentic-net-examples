using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an InterruptMonitor to allow cancellation after a timeout.
        InterruptMonitor monitor = new InterruptMonitor();

        // Start a background thread that will trigger the interrupt after 5 seconds.
        Thread interruptThread = new Thread(() =>
        {
            // Wait for 5,000 milliseconds (5 seconds).
            Thread.Sleep(5000);
            // Signal the monitor to interrupt the ongoing operation.
            monitor.Interrupt();
        });
        interruptThread.IsBackground = true;
        interruptThread.Start();

        try
        {
            // Configure LoadOptions to use the InterruptMonitor during loading.
            LoadOptions loadOptions = new LoadOptions
            {
                InterruptMonitor = monitor
            };

            // Load the Visio diagram with the specified options.
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Assign the same monitor to the Diagram instance for post‑load operations (e.g., PDF conversion).
            diagram.InterruptMonitor = monitor;

            // Prepare PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Example setting: do not export hidden pages.
                ExportHiddenPage = false
                // Additional options can be configured here as needed.
            };

            // Save the diagram as PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}