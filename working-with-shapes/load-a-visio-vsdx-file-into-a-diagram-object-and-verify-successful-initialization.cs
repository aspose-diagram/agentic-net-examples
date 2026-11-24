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
            Console.Error.WriteLine("Please provide the path to a VSDX file as the first argument.");
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
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify successful initialization by checking the page collection
                if (diagram.Pages != null && diagram.Pages.Count > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("Diagram loaded, but no pages were found.");
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