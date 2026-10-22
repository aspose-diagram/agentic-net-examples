using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first command‑line argument to be the path of the VDX file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a VDX file as a command‑line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using the Diagram constructor.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve and display the number of pages in the diagram.
            int pageCount = diagram.Pages.Count;
            Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");

            // Simple validation: check that the diagram contains at least one page.
            if (pageCount == 0)
            {
                Console.Error.WriteLine("Validation failed: Diagram contains no pages.");
            }
            else
            {
                Console.WriteLine("Validation passed: Diagram contains pages.");
            }

            // Release resources held by the Diagram object.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or validation.
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}