using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the Visio file path as a command-line argument.");
            return;
        }

        string filePath = args[0];

        // Guard: verify the file exists before attempting to load
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file path
            Diagram diagram = new Diagram(filePath);

            // Optional: output basic information about the loaded diagram
            Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
        }
        catch (Exception ex)
        {
            // Write any loading errors to the error console
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}