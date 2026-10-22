using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the VSDX file.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Please provide the path to a VSDX file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Verify that the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram was loaded and contains pages.
                int pageCount = diagram.Pages.Count;
                if (pageCount > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");
                }
                else
                {
                    Console.Error.WriteLine("Diagram loaded, but it contains no pages.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}