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
            Console.Error.WriteLine("Please provide the path to a Visio file as a command-line argument.");
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
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the Pages collection.
                int pageCount = diagram.Pages.Count;
                Console.WriteLine($"Diagram loaded successfully. Total pages: {pageCount}");

                // Iterate over each page and output its ID and name.
                foreach (Page page in diagram.Pages)
                {
                    // Page.ID is an integer identifier.
                    int pageId = page.ID;

                    // Page.Name is a string; use it directly.
                    string pageName = page.Name ?? "(Unnamed)";

                    Console.WriteLine($"Page ID: {pageId}, Name: {pageName}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors (loading, parsing, etc.) to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}