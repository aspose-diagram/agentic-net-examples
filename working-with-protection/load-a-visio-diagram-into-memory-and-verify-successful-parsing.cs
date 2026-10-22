using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – \"{inputPath}\"");
            return;
        }

        try
        {
            // Load the Visio diagram into memory.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram was parsed by checking page collection.
            int pageCount = diagram.Pages.Count;
            Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");

            // Optionally, iterate pages and report shape counts.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                int shapeCount = page.Shapes.Count;
                Console.WriteLine($"Page \"{page.Name}\" contains {shapeCount} shape(s).");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during loading or processing.
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}