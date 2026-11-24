using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio file into a Diagram object inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram object was created
                if (diagram == null)
                {
                    Console.Error.WriteLine("Failed to load diagram: diagram instance is null.");
                    return;
                }

                // Verify that the diagram contains at least one page
                if (diagram.Pages == null || diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram loaded but contains no pages.");
                    return;
                }

                // Successful initialization message
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