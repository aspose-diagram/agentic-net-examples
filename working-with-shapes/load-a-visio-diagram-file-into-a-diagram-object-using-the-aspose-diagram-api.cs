using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
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
                // Output basic information about the loaded diagram
                Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}