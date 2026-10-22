using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path specified.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the specified Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found at path '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram object was initialized and contains pages
                if (diagram == null)
                {
                    Console.Error.WriteLine("Error: Diagram object is null after loading.");
                    return;
                }

                // Access the Pages collection to confirm successful load
                int pageCount = diagram.Pages.Count;

                if (pageCount == 0)
                {
                    Console.Error.WriteLine("Error: Diagram loaded but contains no pages.");
                }
                else
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any exceptions that occur during loading
            Console.Error.WriteLine($"Exception occurred while loading diagram: {ex.Message}");
        }
    }
}