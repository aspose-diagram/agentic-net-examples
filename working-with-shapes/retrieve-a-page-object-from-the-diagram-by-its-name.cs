using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageName>");
            return;
        }

        string diagramPath = args[0];
        string pageName = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve the page by its name.
                Page page = diagram.Pages.GetPage(pageName);

                if (page == null)
                {
                    Console.Error.WriteLine($"Page \"{pageName}\" not found in the diagram.");
                    return;
                }

                // Output basic information about the retrieved page.
                Console.WriteLine($"Page found:");
                Console.WriteLine($"  ID   : {page.ID}");
                Console.WriteLine($"  Name : {page.Name}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}