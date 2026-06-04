using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to scan: use first argument or current directory.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Visio file extensions to consider.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };

        // Retrieve all matching files.
        var files = Directory.GetFiles(folderPath)
                             .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                             .ToArray();

        if (files.Length == 0)
        {
            Console.WriteLine("No Visio diagram files found in the specified folder.");
            return;
        }

        Console.WriteLine("Inventory Management Report");
        Console.WriteLine(new string('-', 40));

        foreach (string filePath in files)
        {
            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Count custom properties.
                int customPropCount = diagram.DocumentProps.CustomProps.Count;

                // Output the result.
                Console.WriteLine($"Diagram: {Path.GetFileName(filePath)} | Custom Properties: {customPropCount}");
            }
            catch (Exception ex)
            {
                // Report any loading errors.
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Console.WriteLine(new string('-', 40));
        Console.WriteLine("Report generation completed.");
    }
}
