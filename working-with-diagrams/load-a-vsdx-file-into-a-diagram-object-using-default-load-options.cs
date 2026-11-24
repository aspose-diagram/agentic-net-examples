using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the VSDX file path as the first command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a VSDX file as an argument.");
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
            // Load the diagram using default load options inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Diagram successfully loaded – you can now work with the object
                Console.WriteLine($"Diagram loaded. Page count: {diagram.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}