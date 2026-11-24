using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    // Object used for synchronizing console output
    private static readonly object _consoleLock = new object();

    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Gather Visio files (common extensions)
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vss", ".vstx", ".vst" };
        var files = Directory.GetFiles(inputFolder, "*.*", SearchOption.AllDirectories);
        var visioFiles = new System.Collections.Generic.List<string>();
        foreach (var file in files)
        {
            if (Array.Exists(extensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                visioFiles.Add(file);
            }
        }

        if (visioFiles.Count == 0)
        {
            Console.Error.WriteLine("No Visio files found in the input folder.");
            return;
        }

        // Process files in parallel
        Parallel.ForEach(visioFiles, filePath =>
        {
            // Guard: ensure the file still exists
            if (!File.Exists(filePath))
            {
                lock (_consoleLock)
                {
                    Console.Error.WriteLine($"File not found (skipped): {filePath}");
                }
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Determine output file path (convert to PDF)
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the diagram as PDF
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                // Thread‑safe progress logging
                lock (_consoleLock)
                {
                    Console.WriteLine($"Successfully converted: {filePath} -> {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Thread‑safe error logging
                lock (_consoleLock)
                {
                    Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }
        });

        // Final completion message
        lock (_consoleLock)
        {
            Console.WriteLine("Conversion process completed.");
        }
    }
}