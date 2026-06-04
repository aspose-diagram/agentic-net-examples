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
            Console.Error.WriteLine("Environment variable 'SOURCE_DIR' is not set or empty.");
            return;
        }

        if (string.IsNullOrWhiteSpace(destDir))
        {
            Console.Error.WriteLine("Environment variable 'DEST_DIR' is not set or empty.");
            return;
        }

        // Ensure source directory exists
        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory does not exist: {sourceDir}");
            return;
        }

        // Ensure destination directory exists; create if it does not
        if (!Directory.Exists(destDir))
            Directory.CreateDirectory(destDir);

        // Process each Visio file in the source directory (e.g., .vsdx files)
        string[] diagramFiles = Directory.GetFiles(sourceDir, "*.vsdx");

        foreach (string sourceFilePath in diagramFiles)
        {
            try
            {
                // Load the diagram from the source file
                using (Diagram diagram = new Diagram(sourceFilePath))
                {
                    // Determine the output file path in the destination directory
                    string fileName = Path.GetFileName(sourceFilePath);
                    string destFilePath = Path.Combine(destDir, fileName);

                    // Save the diagram to the destination path using the same format
                    diagram.Save(destFilePath, SaveFileFormat.Vsdx);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{sourceFilePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Diagram processing completed successfully.");
    }
}