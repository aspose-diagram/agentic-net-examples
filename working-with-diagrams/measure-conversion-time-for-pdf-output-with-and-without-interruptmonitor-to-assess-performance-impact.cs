using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file and the PDF outputs
            string sourceFile = "input.vsdx";
            string outputWithoutInterrupt = "output_without_interrupt.pdf";
            string outputWithInterrupt = "output_with_interrupt.pdf";

            // ------------------------------
            // Conversion without InterruptMonitor
            // ------------------------------
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Load the diagram using default options (no interrupt monitor)
            Diagram diagram = new Diagram(sourceFile);

            // Save to PDF
            diagram.Save(outputWithoutInterrupt, SaveFileFormat.Pdf);

            stopwatch.Stop();
            Console.WriteLine($"Conversion without InterruptMonitor: {stopwatch.ElapsedMilliseconds} ms");

            // ------------------------------
            // Conversion with InterruptMonitor
            // ------------------------------
            stopwatch.Restart();

            // Create LoadOptions and assign an InterruptMonitor
            LoadOptions loadOptions = new LoadOptions
            {
                InterruptMonitor = new InterruptMonitor()
            };

            // Load the diagram with the interrupt monitor enabled
            Diagram diagramWithInterrupt = new Diagram(sourceFile, loadOptions);

            // (Optional) also set the diagram's InterruptMonitor property
            diagramWithInterrupt.InterruptMonitor = new InterruptMonitor();

            // Save to PDF
            diagramWithInterrupt.Save(outputWithInterrupt, SaveFileFormat.Pdf);

            stopwatch.Stop();
            Console.WriteLine($"Conversion with InterruptMonitor: {stopwatch.ElapsedMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
