using System;
using System.IO;
using System.Linq;               // Required for LINQ extension methods used in file filtering
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the folder containing Visio files
        Console.Write("Enter the full path to the folder with Visio diagrams: ");
        string folderPath = Console.ReadLine();

        // Validate the provided folder path
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Invalid folder path.");
            return;
        }

        // Supported Visio extensions
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

        // Collect all matching files using LINQ for case‑insensitive extension check
        var diagramFiles = Directory.GetFiles(folderPath)
                                    .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                    .ToArray();

        // Ensure at least one diagram was found
        if (diagramFiles.Length == 0)
        {
            Console.Error.WriteLine("No Visio diagram files found in the specified folder.");
            return;
        }

        Console.WriteLine("\nHeader Report:");
        Console.WriteLine("==============");

        // Iterate over each discovered diagram file
        foreach (string filePath in diagramFiles)
        {
            // Guard to ensure the file still exists before processing
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram from the file
                Diagram diagram = new Diagram(filePath);

                // Retrieve header texts, using empty string fallback if null
                string leftHeader   = diagram.HeaderFooter.HeaderLeft   ?? string.Empty;
                string centerHeader = diagram.HeaderFooter.HeaderCenter ?? string.Empty;
                string rightHeader  = diagram.HeaderFooter.HeaderRight  ?? string.Empty;

                // Output the report line for this diagram
                Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Left Header:   {leftHeader}");
                Console.WriteLine($"  Center Header: {centerHeader}");
                Console.WriteLine($"  Right Header:  {rightHeader}");
                Console.WriteLine();

                // Example of a batch modification (optional):
                // diagram.HeaderFooter.HeaderLeft   = "Updated Left";
                // diagram.HeaderFooter.HeaderCenter = "Updated Center";
                // diagram.HeaderFooter.HeaderRight  = "Updated Right";
                // diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the current file
                Console.Error.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Report generation completed.");
    }
}