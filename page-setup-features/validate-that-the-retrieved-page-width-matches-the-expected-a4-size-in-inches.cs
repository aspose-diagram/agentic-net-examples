using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: verify that the file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Retrieve the first page.
            Page page = diagram.Pages[0];

            // Get the page width in inches.
            double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

            // Expected A4 width in inches (8.27 inches).
            const double expectedA4Width = 8.27;

            // Validate the width (allowing a tiny tolerance for floating‑point differences).
            const double tolerance = 0.001;
            if (Math.Abs(pageWidthInches - expectedA4Width) > tolerance)
            {
                Console.Error.WriteLine($"Validation failed: Page width is {pageWidthInches:F3} inches, expected {expectedA4Width:F3} inches.");
            }
            else
            {
                Console.WriteLine($"Validation succeeded: Page width matches A4 size ({expectedA4Width} inches).");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}