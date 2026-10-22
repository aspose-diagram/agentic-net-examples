using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory to process: use first argument or current directory.
        string targetDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Guard: ensure the directory exists.
        if (!Directory.Exists(targetDirectory))
        {
            Console.Error.WriteLine($"Directory does not exist: {targetDirectory}");
            return;
        }

        // Get all Visio files (VSDX) in the directory.
        string[] diagramFiles = Directory.GetFiles(targetDirectory, "*.vsdx", SearchOption.TopDirectoryOnly);

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio (.vsdx) files found to process.");
            return;
        }

        foreach (string filePath in diagramFiles)
        {
            // Guard: ensure the file exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram from the file.
                Diagram diagram = new Diagram(filePath);

                // Update the footer text with the current timestamp.
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                diagram.HeaderFooter.FooterRight = $"Generated on {timestamp}";

                // Save the diagram back to the same file (overwrites original).
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading, updating, or saving.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}