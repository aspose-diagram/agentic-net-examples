using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Open the file as a stream and load the Visio diagram.
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (Diagram diagram = new Diagram(stream))
            {
                // Verify that the diagram contains at least one page.
                int pageCount = diagram.Pages.Count;
                if (pageCount > 0)
                {
                    Console.WriteLine($"Success: Diagram loaded with {pageCount} page(s).");
                }
                else
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any exceptions that occur during loading or verification.
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}