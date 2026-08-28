using System.IO;
using System;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Create an interrupt monitor instance
            InterruptMonitor monitor = new InterruptMonitor();

            // Start a background task that will trigger an interruption after 5 seconds
            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                monitor.Interrupt(); // Request interruption
            });

            // Assign the monitor to the diagram before starting the conversion
            diagram.InterruptMonitor = monitor;

            // Prepare PDF save options (default settings)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram as PDF; the operation will be interrupted if it exceeds 5 seconds
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
