using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory containing diagram files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing diagram files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the directory exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("The specified folder does not exist: " + folderPath);
            return;
        }

        // Define supported Visio file extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vssx", ".vstx", ".vss", ".vst", ".vdx", ".vtx" };

        // Retrieve all files with supported extensions.
        string[] allFiles = Directory.GetFiles(folderPath);
        foreach (string filePath in allFiles)
        {
            // Filter by extension.
            string ext = Path.GetExtension(filePath);
            if (Array.IndexOf(supportedExtensions, ext, 0, supportedExtensions.Length) < 0)
                continue;

            // Guard: ensure the file exists (redundant but per requirements).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Retrieve the count of custom document properties.
                int customPropCount = diagram.DocumentProps?.CustomProps?.Count ?? 0;

                // Output the result.
                Console.WriteLine($"{Path.GetFileName(filePath)}: {customPropCount} custom properties");

                // Dispose the diagram to free resources.
                diagram.Dispose();
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing this file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}