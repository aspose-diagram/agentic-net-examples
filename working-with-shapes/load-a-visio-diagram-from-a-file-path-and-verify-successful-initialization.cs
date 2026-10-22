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
            Console.Error.WriteLine("Please provide the Visio file path as an argument.");
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
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(filePath))
            {
                // Verify successful initialization by checking the number of pages
                int pageCount = diagram.Pages.Count;
                Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}