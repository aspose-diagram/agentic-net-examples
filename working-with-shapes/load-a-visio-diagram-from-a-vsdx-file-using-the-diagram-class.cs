using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example operation: output basic information about the loaded diagram.
                Console.WriteLine($"Diagram loaded successfully from '{inputPath}'.");
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
                Console.WriteLine($"Number of masters: {diagram.Masters.Count}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading.
            Console.Error.WriteLine($"An error occurred while loading the diagram: {ex.Message}");
        }
    }
}