using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a directory path argument was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: Please provide the path to the directory containing diagram files as the first argument.");
            return;
        }

        string directoryPath = args[0];

        // Guard: ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Error: Directory does not exist: {directoryPath}");
            return;
        }

        // Define supported Visio file extensions
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vss", ".vst", ".vsx", ".vtx" };

        // Retrieve all files with supported extensions
        string[] diagramFiles = Directory.GetFiles(directoryPath);
        bool anyFileFound = false;

        foreach (string filePath in diagramFiles)
        {
            // Filter by supported extensions (case-insensitive)
            if (Array.IndexOf(supportedExtensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            anyFileFound = true;

            // Guard: ensure the file exists (redundant after GetFiles but required by rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Error: File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Count custom document properties
                    int customPropCount = diagram.DocumentProps?.CustomProps?.Count ?? 0;

                    // Output the result
                    Console.WriteLine($"{Path.GetFileName(filePath)}: {customPropCount} custom properties");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing the diagram
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        if (!anyFileFound)
        {
            Console.Error.WriteLine("No supported diagram files were found in the specified directory.");
        }
    }
}