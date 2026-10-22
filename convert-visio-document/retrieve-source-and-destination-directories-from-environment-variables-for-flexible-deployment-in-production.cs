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

        // Verify that the source directory exists
        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory does not exist: {sourceDir}");
            return;
        }

        // Ensure the destination directory exists; create it if necessary
        if (!Directory.Exists(destDir))
        {
            try
            {
                Directory.CreateDirectory(destDir);
                Console.WriteLine($"Created destination directory: {destDir}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create destination directory '{destDir}': {ex.Message}");
                return;
            }
        }

        // Get all Visio files (VSDX) in the source directory
        string[] diagramFiles = Directory.GetFiles(sourceDir, "*.vsdx");

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio (.vsdx) files found in the source directory.");
            return;
        }

        // Process each diagram file
        foreach (string sourceFilePath in diagramFiles)
        {
            // Guard: ensure the file exists before processing
            if (!File.Exists(sourceFilePath))
            {
                Console.Error.WriteLine($"File not found: {sourceFilePath}");
                continue;
            }

            string fileName = Path.GetFileName(sourceFilePath);
            string destFilePath = Path.Combine(destDir, fileName);

            try
            {
                // Load the diagram from the source file
                Diagram diagram = new Diagram(sourceFilePath);

                // Save the diagram to the destination directory using VSDX format
                diagram.Save(destFilePath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Successfully copied '{fileName}' to destination.");
            }
            catch (Exception ex)
            {
                // Capture any Aspose.Diagram related errors
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}