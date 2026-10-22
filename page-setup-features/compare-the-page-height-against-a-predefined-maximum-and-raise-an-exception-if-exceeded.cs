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
            Console.Error.WriteLine("Usage: Program <diagram-file-path> [max-page-height]");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Determine the maximum allowed page height (in inches). Default to A4 height if not provided.
        double maxPageHeight = 11.69; // Default A4 height in inches.
        if (args.Length > 1)
        {
            if (!double.TryParse(args[1], out maxPageHeight))
            {
                Console.Error.WriteLine($"Error: Invalid max page height value '{args[1]}'.");
                return;
            }
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page and compare its height to the maximum.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page height in inches.
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // If the page height exceeds the predefined maximum, raise an exception.
                if (pageHeight > maxPageHeight)
                {
                    throw new Exception($"Page ID {page.ID} height {pageHeight} exceeds the maximum allowed {maxPageHeight} inches.");
                }
            }

            Console.WriteLine("All pages are within the allowed height limit.");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}