using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Please provide the Visio file path as a command-line argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(filePath))
            {
                // Diagram successfully loaded; you can now work with the object
                Console.WriteLine("Diagram loaded successfully.");
                // Example: output the number of pages in the diagram
                Console.WriteLine($"Page count: {diagram.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            // Write any loading errors to the error console
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}