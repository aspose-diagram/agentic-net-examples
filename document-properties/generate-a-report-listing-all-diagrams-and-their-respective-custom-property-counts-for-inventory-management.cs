using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to scan for Visio diagram files.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Supported Visio file extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx" };

        // Collect all diagram files in the folder (non‑recursive).
        var diagramFiles = Directory.GetFiles(folderPath)
                                    .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                    .ToArray();

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio diagram files found in the specified folder.");
            return;
        }

        Console.WriteLine("Inventory Management Report");
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Folder: {folderPath}");
        Console.WriteLine();

        foreach (var filePath in diagramFiles)
        {
            // Guard to ensure the file actually exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram using the constructor that accepts a file path.
                Diagram diagram = new Diagram(filePath);

                // Count custom properties at the document level.
                int customPropCount = diagram.DocumentProps.CustomProps.Count;

                Console.WriteLine($"Diagram: {Path.GetFileName(filePath)}");
                Console.WriteLine($"Custom Property Count: {customPropCount}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                // Report any errors but continue processing other files.
                Console.Error.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                Console.Error.WriteLine();
            }
        }

        Console.WriteLine("Report generation completed.");
    }
}