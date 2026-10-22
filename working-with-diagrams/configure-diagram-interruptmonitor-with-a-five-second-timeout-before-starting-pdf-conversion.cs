using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Create an InterruptMonitor instance
            InterruptMonitor monitor = new InterruptMonitor();

            // Start a background thread that will interrupt after 5 seconds
            Thread interruptThread = new Thread(() =>
            {
                // Wait for 5 seconds (5000 milliseconds)
                Thread.Sleep(5000);
                // Trigger interruption
                monitor.Interrupt();
            });
            interruptThread.IsBackground = true;
            interruptThread.Start();

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assign the monitor to the diagram to enable interruption during conversion
            diagram.InterruptMonitor = monitor;

            // Configure PDF save options (optional settings can be added here)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Perform the PDF conversion
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}