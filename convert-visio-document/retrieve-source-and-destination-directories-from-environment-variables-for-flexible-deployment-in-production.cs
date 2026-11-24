using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Retrieve source and destination directories from environment variables
        string sourceDir = Environment.GetEnvironmentVariable("SOURCE_DIR");
        string destDir = Environment.GetEnvironmentVariable("DEST_DIR");

        // Validate that the environment variables are set
        if (string.IsNullOrWhiteSpace(sourceDir))
        {
            Console.Error.WriteLine("Environment variable 'SOURCE_DIR' is not set.");
            return;
        }

        if (string.IsNullOrWhiteSpace(destDir))
        {
            Console.Error.WriteLine("Environment variable 'DEST_DIR' is not set.");
            return;
        }

        // Validate that the directories exist
        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory does not exist: {sourceDir}");
            return;
        }

        if (!Directory.Exists(destDir))
        {
            Console.Error.WriteLine($"Destination directory does not exist: {destDir}");
            return;
        }

        // Process each Visio file in the source directory
        string[] visioFiles = Directory.GetFiles(sourceDir, "*.vsdx");
        if (visioFiles.Length == 0)
        {
            Console.WriteLine("No Visio files (*.vsdx) found in the source directory.");
            return;
        }

        foreach (string filePath in visioFiles)
        {
            // Guard against missing file (should not happen as we got the list from GetFiles)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Determine output PDF path
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(destDir, fileNameWithoutExt + ".pdf");

                // Save the diagram as PDF
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                Console.WriteLine($"Successfully converted '{filePath}' to PDF at '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}