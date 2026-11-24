using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagrams.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing Visio diagrams: ");
            folderPath = Console.ReadLine();
        }

        // Guard: folder must exist.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define supported Visio file extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vss", ".vst", ".vssm", ".vstm", ".vsdm" };

        // Collect all matching files.
        var diagramFiles = Directory.GetFiles(folderPath);
        int processedCount = 0;
        int failedCount = 0;

        foreach (var filePath in diagramFiles)
        {
            // Process only supported extensions.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Guard: file must exist (redundant but per rule).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Set uniform header margin to 0.25 inches.
                    diagram.HeaderFooter.HeaderMargin.Value = 0.25;

                    // Save the diagram back to the same file (overwrites original).
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                processedCount++;
            }
            catch (Exception ex)
            {
                // Log any failure for this file.
                Console.Error.WriteLine($"Failed to process '{filePath}': {ex.Message}");
                failedCount++;
            }
        }

        // Summary output.
        Console.WriteLine($"Processing completed. Successfully updated: {processedCount}, Failed: {failedCount}");
    }
}