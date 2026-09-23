using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class PdfConversionPerformance
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file and output PDFs
            string sourcePath = "input.vsdx";
            string outputPdfWithout = "output_without_interrupt.pdf";
            string outputPdfWith = "output_with_interrupt.pdf";

            // Measure conversion time without InterruptMonitor
            Diagram diagramWithout = new Diagram(sourcePath);
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            diagramWithout.Save(outputPdfWithout, SaveFileFormat.Pdf);
            stopwatch.Stop();

            Console.WriteLine($"Conversion time without InterruptMonitor: {stopwatch.ElapsedMilliseconds} ms");

            // Measure conversion time with InterruptMonitor
            Diagram diagramWith = new Diagram(sourcePath);
            diagramWith.InterruptMonitor = new InterruptMonitor(); // Enable interrupt monitoring

            stopwatch.Restart();
            diagramWith.Save(outputPdfWith, SaveFileFormat.Pdf);
            stopwatch.Stop();

            Console.WriteLine($"Conversion time with InterruptMonitor: {stopwatch.ElapsedMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
