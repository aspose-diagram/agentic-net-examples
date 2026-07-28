using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PdfConversionPerformance
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = "input.vsdx";

            // Output PDF files
            string outputWithoutInterrupt = "output_without_interrupt.pdf";
            string outputWithInterrupt = "output_with_interrupt.pdf";

            // ------------------------------
            // Conversion without InterruptMonitor
            // ------------------------------
            // Load the diagram with default LoadOptions (no interrupt monitor)
            Diagram diagramWithoutInterrupt = new Diagram(sourceFile);

            // Measure the time taken to save as PDF
            Stopwatch swWithout = Stopwatch.StartNew();
            diagramWithoutInterrupt.Save(outputWithoutInterrupt, SaveFileFormat.Pdf);
            swWithout.Stop();

            Console.WriteLine($"Conversion without InterruptMonitor took: {swWithout.ElapsedMilliseconds} ms");

            // ------------------------------
            // Conversion with InterruptMonitor
            // ------------------------------
            // Create a LoadOptions instance and assign an InterruptMonitor
            LoadOptions loadOptions = new LoadOptions
            {
                InterruptMonitor = new InterruptMonitor()
            };

            // Load the diagram using the LoadOptions that includes the interrupt monitor
            Diagram diagramWithInterrupt = new Diagram(sourceFile, loadOptions);

            // Measure the time taken to save as PDF
            Stopwatch swWith = Stopwatch.StartNew();
            diagramWithInterrupt.Save(outputWithInterrupt, SaveFileFormat.Pdf);
            swWith.Stop();

            Console.WriteLine($"Conversion with InterruptMonitor took: {swWith.ElapsedMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
