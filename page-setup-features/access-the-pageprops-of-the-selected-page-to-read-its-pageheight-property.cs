using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path> [page-index]");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Optional page index argument; default to the first page (index 0).
        int pageIndex = 0;
        if (args.Length > 1 && !int.TryParse(args[1], out pageIndex))
        {
            Console.Error.WriteLine("Error: Invalid page index argument.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Validate the requested page index.
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} page(s).");
                return;
            }

            // Access the selected page.
            Page page = diagram.Pages[pageIndex];

            // Read the PageHeight property (value is in inches).
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Output the retrieved height.
            Console.WriteLine($"Page {pageIndex} Height: {pageHeight} inches");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}