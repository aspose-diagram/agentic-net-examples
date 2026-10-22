using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and page name
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageName>");
            return;
        }

        string diagramPath = args[0];
        string pageName = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve the page by its name
                Page page = diagram.Pages.GetPage(pageName);

                if (page == null)
                {
                    Console.WriteLine($"Page with name \"{pageName}\" was not found.");
                }
                else
                {
                    // Output basic information about the found page
                    Console.WriteLine($"Page found: Name = {page.NameU}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}