using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Access the page using explicit type (no var in foreach).
                Aspose.Diagram.Page page = diagram.Pages[i];

                // Retrieve the ScaleX value from the page's print properties.
                double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                // List pages where ScaleX is not equal to 1.0.
                if (scaleX != 1.0)
                {
                    Console.WriteLine($"Page index {i} (ID {page.ID}) has ScaleX = {scaleX}");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}