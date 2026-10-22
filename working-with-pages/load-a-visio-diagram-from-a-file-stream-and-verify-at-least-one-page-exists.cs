using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure an input file path is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path specified.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Open a file stream for reading the Visio diagram
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream inside a using block as required
                using (Diagram diagram = new Diagram(stream))
                {
                    // Verify that the diagram contains at least one page
                    if (diagram.Pages != null && diagram.Pages.Count > 0)
                    {
                        Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                    }
                    else
                    {
                        Console.Error.WriteLine("Error: The loaded diagram contains no pages.");
                    }
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