using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <path to VSDX file>");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages == null || diagram.Pages.Count == 0)
                {
                    throw new Exception("Diagram loaded but contains no pages.");
                }

                // Output a success message with basic information
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