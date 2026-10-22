using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure an input file path is provided as a command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a VDX file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify that the specified file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages != null && diagram.Pages.Count > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                }
                else
                {
                    Console.Error.WriteLine("Diagram loaded but contains no pages.");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}