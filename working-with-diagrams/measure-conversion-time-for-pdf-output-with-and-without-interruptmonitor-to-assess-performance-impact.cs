using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: check output directory existence
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // ------------------------------
        // Conversion without InterruptMonitor
        // ------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare PDF save options (default settings)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                string outputPath = Path.Combine(outputDir, "output_without_monitor.pdf");

                Stopwatch sw = Stopwatch.StartNew();

                // Save diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                sw.Stop();

                Console.WriteLine($"Conversion without InterruptMonitor took {sw.ElapsedMilliseconds} ms.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion without InterruptMonitor: {ex.Message}");
            return;
        }

        // ------------------------------
        // Conversion with InterruptMonitor
        // ------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assign an InterruptMonitor to the diagram
                InterruptMonitor monitor = new InterruptMonitor();
                diagram.InterruptMonitor = monitor;

                // Prepare PDF save options (default settings)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                string outputPath = Path.Combine(outputDir, "output_with_monitor.pdf");

                Stopwatch sw = Stopwatch.StartNew();

                // Save diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                sw.Stop();

                Console.WriteLine($"Conversion with InterruptMonitor took {sw.ElapsedMilliseconds} ms.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion with InterruptMonitor: {ex.Message}");
            return;
        }
    }
}