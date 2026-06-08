using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // ------------------------------
            // Conversion without InterruptMonitor
            // ------------------------------
            Stopwatch sw = Stopwatch.StartNew();

            // Save the diagram as PDF (no interrupt monitor attached)
            diagram.Save("output_without_monitor.pdf", SaveFileFormat.Pdf);

            sw.Stop();
            Console.WriteLine($"Conversion without InterruptMonitor: {sw.ElapsedMilliseconds} ms");

            // ------------------------------
            // Conversion with InterruptMonitor
            // ------------------------------
            // Attach the default InterruptMonitor (does not request interruption)
            diagram.InterruptMonitor = new InterruptMonitor();

            sw.Restart();

            // Save the diagram as PDF with the monitor attached
            diagram.Save("output_with_monitor.pdf", SaveFileFormat.Pdf);

            sw.Stop();
            Console.WriteLine($"Conversion with InterruptMonitor: {sw.ElapsedMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
