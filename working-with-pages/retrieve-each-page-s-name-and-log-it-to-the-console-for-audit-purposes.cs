using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            Console.Error.WriteLine("Usage: Program <path-to-visio-file>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the specified file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Retrieve page ID and name (NameU is the universal name of the page).
                int pageId = page.ID;
                string pageName = page.NameU;

                // Log page information to the console.
                Console.WriteLine($"Page ID: {pageId}, Name: {pageName}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}