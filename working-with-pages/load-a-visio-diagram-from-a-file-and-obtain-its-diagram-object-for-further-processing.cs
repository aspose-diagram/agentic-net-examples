using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Diagram successfully loaded – you can now process it
                Console.WriteLine($"Diagram loaded. Total pages: {diagram.Pages.Count}");

                // Example: iterate through pages and display their names
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    var page = diagram.Pages[i];
                    Console.WriteLine($"Page {i + 1}: {page.Name}");
                }

                // The 'diagram' object is available here for any further processing
                // Add your custom logic below
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}