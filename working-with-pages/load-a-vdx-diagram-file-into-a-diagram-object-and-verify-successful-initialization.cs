using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the VDX file path as the first argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <path-to-vdx-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
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
                // Verify that the diagram was initialized correctly.
                // A basic check is to ensure it contains at least one page.
                if (diagram.Pages == null || diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram loaded but contains no pages.");
                }
                else
                {
                    Console.WriteLine("Diagram loaded successfully.");
                    Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading.
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}