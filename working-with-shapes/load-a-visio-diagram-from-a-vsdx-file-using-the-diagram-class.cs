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
            // Load the Visio diagram from the specified VSDX file
            Diagram diagram = new Diagram(inputPath);

            // Output basic information to confirm successful loading
            Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
        }
        catch (Exception ex)
        {
            // Write any loading errors to the error stream
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}