using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that an input file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram was initialized and contains at least one page
                if (diagram.Pages == null || diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram loaded but contains no pages.");
                    return;
                }

                // Output success information
                Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}