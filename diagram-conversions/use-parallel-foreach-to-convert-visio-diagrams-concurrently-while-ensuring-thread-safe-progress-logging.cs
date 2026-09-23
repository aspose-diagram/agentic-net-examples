using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using System.Collections.Concurrent;

class Program
{
    // Thread‑safe logger using a lock object
    private static readonly object _logLock = new object();

    static void Main(string[] args)
    {
        // Input and output directories can be passed as arguments
        string inputDir = args.Length > 0 ? args[0] : "InputDiagrams";
        string outputDir = args.Length > 1 ? args[1] : "ConvertedDiagrams";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all Visio files to process
        string[] visioFiles = Directory.GetFiles(inputDir, "*.vsdx", SearchOption.AllDirectories);

        // Process files in parallel
        Parallel.ForEach(visioFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, filePath =>
        {
            try
            {
                // Load the Visio diagram (using the provided load rule)
                Diagram diagram = new Diagram(filePath);

                // Determine output file path (PDF format as an example)
                string outputFile = Path.Combine(outputDir,
                    Path.GetFileNameWithoutExtension(filePath) + ".pdf");

                // Save the diagram (using the provided save rule)
                diagram.Save(outputFile, SaveFileFormat.Pdf);

                // Thread‑safe progress logging
                lock (_logLock)
                {
                    Console.WriteLine($"[SUCCESS] Converted: {filePath} -> {outputFile}");
                }
            }
            catch (Exception ex)
            {
                // Thread‑safe error logging
                lock (_logLock)
                {
                    Console.WriteLine($"[ERROR] Failed to convert {filePath}: {ex.Message}");
                }
            }
        });
    }
}
