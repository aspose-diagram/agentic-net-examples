using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchConverter
{
    // Object used for synchronizing console output
    private static readonly object _consoleLock = new object();

    static void Main()
    {
        // Define source Visio files (could be populated dynamically)
        List<string> sourceFiles = new List<string>
        {
            @"C:\Visio\Input\Diagram1.vsdx",
            @"C:\Visio\Input\Diagram2.vsdx",
            @"C:\Visio\Input\Diagram3.vsdx"
        };

        // Destination folder for converted PDFs
        string outputFolder = @"C:\Visio\Output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Parallel conversion
        Parallel.ForEach(sourceFiles, sourcePath =>
        {
            try
            {
                // Load the diagram using the constructor that accepts a file path
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Build output file name (same base name, .pdf extension)
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(sourcePath) + ".pdf");

                    // Save the diagram in PDF format using the Save method with SaveFileFormat
                    diagram.Save(outputPath, SaveFileFormat.Pdf);
                }

                // Thread‑safe progress logging
                lock (_consoleLock)
                {
                    Console.WriteLine($"Successfully converted: {Path.GetFileName(sourcePath)}");
                }
            }
            catch (Exception ex)
            {
                // Thread‑safe error logging
                lock (_consoleLock)
                {
                    Console.WriteLine($"Error converting {Path.GetFileName(sourcePath)}: {ex.Message}");
                }
            }
        });

        // Final completion message
        Console.WriteLine("Batch conversion completed.");
    }
}
