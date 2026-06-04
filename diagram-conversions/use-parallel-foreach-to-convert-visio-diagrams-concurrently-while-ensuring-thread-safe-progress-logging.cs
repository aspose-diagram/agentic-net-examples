using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Object used to synchronize console output across threads
    private static readonly object consoleLock = new object();

    static void Main()
    {
        try
        {

            // Input folder containing Visio files
            string inputFolder = @"C:\Visio\Input";
            // Output folder for converted PDFs
            string outputFolder = @"C:\Visio\Output";

            // Ensure output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Get all Visio files (any supported extension) in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            // Filter to known Visio extensions
            visioFiles = Array.FindAll(visioFiles, f =>
                f.EndsWith(".vsdx", StringComparison.OrdinalIgnoreCase) ||
                f.EndsWith(".vsd", StringComparison.OrdinalIgnoreCase) ||
                f.EndsWith(".vdx", StringComparison.OrdinalIgnoreCase) ||
                f.EndsWith(".vsx", StringComparison.OrdinalIgnoreCase) ||
                f.EndsWith(".vtx", StringComparison.OrdinalIgnoreCase));

            // Process each file in parallel
            Parallel.ForEach(visioFiles, visioPath =>
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(visioPath);

                    // Prepare PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        DefaultFont = "Arial",
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Determine output file name
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(visioPath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                    // Save the diagram as PDF
                    diagram.Save(outputPath, pdfOptions);

                    // Thread‑safe progress logging
                    lock (consoleLock)
                    {
                        Console.WriteLine($"Successfully converted: {visioPath} -> {outputPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Thread‑safe error logging
                    lock (consoleLock)
                    {
                        Console.WriteLine($"Error processing '{visioPath}': {ex.Message}");
                    }
                }
            });

            // Final message
            Console.WriteLine("Batch conversion completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
