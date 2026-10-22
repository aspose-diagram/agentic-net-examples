using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram and log its name
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Page.NameU holds the universal (non-localized) name of the page
                Console.WriteLine($"Page Name: {page.NameU}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}