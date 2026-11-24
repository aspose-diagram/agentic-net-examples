using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and list those with ScaleX != 1.0.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Access the page using explicit type.
                Page page = diagram.Pages[i];

                // Retrieve the ScaleX value from the page's print properties.
                double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                // Check if ScaleX differs from the default of 1.0.
                if (scaleX != 1.0)
                {
                    // Output page index (1‑based) and name for clarity.
                    Console.WriteLine($"Page {i + 1} (Name: {page.Name}) has ScaleX = {scaleX}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}