using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: check input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: check output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory not found: {outputDir}");
            return;
        }

        string outputWithMonitor = Path.Combine(outputDir, "output_with_monitor.pdf");
        string outputWithoutMonitor = Path.Combine(outputDir, "output_without_monitor.pdf");

        // ------------------------------
        // Conversion WITH InterruptMonitor
        // ------------------------------
        try
        {
            // Create an InterruptMonitor instance.
            InterruptMonitor monitor = new InterruptMonitor();

            // Prepare LoadOptions with the monitor.
            LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
            loadOptions.InterruptMonitor = monitor;

            // Load diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath, loadOptions))
            {
                // Assign the same monitor to the diagram for post‑load operations.
                diagram.InterruptMonitor = monitor;

                // Configure PDF save options (default settings are sufficient for timing).
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Measure conversion time.
                Stopwatch sw = Stopwatch.StartNew();
                diagram.Save(outputWithMonitor, pdfOptions);
                sw.Stop();

                Console.WriteLine($"PDF conversion WITH InterruptMonitor took {sw.ElapsedMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion WITH InterruptMonitor: {ex.Message}");
        }

        // ------------------------------
        // Conversion WITHOUT InterruptMonitor
        // ------------------------------
        try
        {
            // Load diagram without any monitor.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options (default settings).
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Measure conversion time.
                Stopwatch sw = Stopwatch.StartNew();
                diagram.Save(outputWithoutMonitor, pdfOptions);
                sw.Stop();

                Console.WriteLine($"PDF conversion WITHOUT InterruptMonitor took {sw.ElapsedMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion WITHOUT InterruptMonitor: {ex.Message}");
        }
    }
}