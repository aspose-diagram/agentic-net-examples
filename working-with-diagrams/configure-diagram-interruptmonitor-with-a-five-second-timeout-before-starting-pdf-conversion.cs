using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths to the source Visio file and the target PDF file
        string inputPath = "input.vsdx";
        // Guard against missing input file
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.pdf";

        // Create an InterruptMonitor to allow early termination
        InterruptMonitor monitor = new InterruptMonitor();

        // Start a background task that will interrupt after 5 seconds
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            monitor.Interrupt(); // Signal interruption if operation exceeds timeout
        });

        try
        {
            // Prepare load options and assign the monitor for load interruption
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
            loadOptions.InterruptMonitor = monitor;

            // Load the diagram with the interrupt monitor attached
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Assign the same monitor to the diagram for post‑load operations (e.g., PDF conversion)
            diagram.InterruptMonitor = monitor;

            // Configure PDF save options (default font can be set as needed)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Handle any errors, including those caused by interruption
            Console.Error.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
    }
}