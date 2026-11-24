using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <path-to-diagram>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Retrieve page ID
                long pageId = page.ID;

                // Retrieve page name (use universal name if available)
                string pageName = page.NameU;

                // Determine if the page is a background page (BOOL enum)
                bool isBackground = page.Background == BOOL.True;

                // Log the page information
                Console.WriteLine($"Page ID: {pageId}, Name: {pageName}, IsBackground: {isBackground}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}