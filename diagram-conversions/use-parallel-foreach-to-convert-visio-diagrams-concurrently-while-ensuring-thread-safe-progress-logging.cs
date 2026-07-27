using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchConverter
{
    static void Main()
    {
        // List of Visio files to convert
        var inputFiles = new List<string>
        {
            @"C:\Visio\Input1.vsdx",
            @"C:\Visio\Input2.vsdx",
            // Add more file paths as needed
        };

        // Destination folder for converted files
        string outputFolder = @"C:\Visio\Converted";

        ConvertVisioFilesConcurrently(inputFiles, outputFolder);
    }

    static void ConvertVisioFilesConcurrently(IEnumerable<string> inputFiles, string outputFolder)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Thread‑safe collection for progress messages
        var progressLog = new ConcurrentBag<string>();

        // Process each file in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                // Load the diagram using the constructor that accepts a file name
                using (var diagram = new Diagram(inputPath))
                {
                    // Build the output file name (same base name, .pdf extension)
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

                    // Save the diagram using the Save method with SaveFileFormat enum
                    diagram.Save(outputPath, SaveFileFormat.Pdf);

                    progressLog.Add($"Converted: {inputPath} → {outputPath}");
                }
            }
            catch (Exception ex)
            {
                progressLog.Add($"Failed: {inputPath} – {ex.Message}");
            }
        });

        // Output the progress log in a thread‑safe manner
        foreach (var message in progressLog)
        {
            Console.WriteLine(message);
        }
    }
}
