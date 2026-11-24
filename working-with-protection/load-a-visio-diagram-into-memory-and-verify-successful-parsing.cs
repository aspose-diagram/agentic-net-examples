using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify that the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page.
                if (diagram.Pages.Count > 0)
                {
                    Console.WriteLine("Visio diagram loaded successfully. Page count: " + diagram.Pages.Count);
                }
                else
                {
                    // If no pages are present, consider it a parsing failure.
                    Console.Error.WriteLine("Error: Diagram loaded but contains no pages.");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any exceptions that occur during loading.
            Console.Error.WriteLine("Error loading Visio diagram: " + ex.Message);
        }
    }
}