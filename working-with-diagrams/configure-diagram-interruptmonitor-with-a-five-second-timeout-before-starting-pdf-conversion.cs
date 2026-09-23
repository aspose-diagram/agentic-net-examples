using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Create an InterruptMonitor instance
            InterruptMonitor monitor = new InterruptMonitor();

            // Set up a timer that will trigger an interrupt after 5 seconds
            using (Timer timer = new Timer(_ => monitor.Interrupt(), null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan))
            {
                // Assign the monitor to the diagram to enable interruption during conversion
                diagram.InterruptMonitor = monitor;

                // Configure PDF save options (optional settings can be added here)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF; the timer will interrupt if the operation exceeds 5 seconds
                diagram.Save("output.pdf", pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}