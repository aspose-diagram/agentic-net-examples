using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [PageIndex]");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        // Determine which page to read (default to first page)
        int pageIndex = 0;
        if (args.Length > 1 && int.TryParse(args[1], out int parsedIndex))
        {
            pageIndex = parsedIndex;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Validate page index range
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Document contains {diagram.Pages.Count} page(s).");
                return;
            }

            // Access the selected page
            Page page = diagram.Pages[pageIndex];

            // Read the PageHeight property (value is in inches)
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Output the result
            Console.WriteLine($"Page {pageIndex} Height: {pageHeight} inches");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}