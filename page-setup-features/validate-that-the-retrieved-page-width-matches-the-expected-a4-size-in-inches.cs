using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Get the page width in inches
            double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

            // Expected A4 width in inches (approximately 8.27")
            const double expectedA4Width = 8.27;

            // Allow a small tolerance for floating‑point differences
            const double tolerance = 0.01;

            // Validate the width against the expected A4 size
            if (Math.Abs(pageWidthInches - expectedA4Width) <= tolerance)
            {
                Console.WriteLine($"Success: Page width ({pageWidthInches:F2}\" ) matches expected A4 width ({expectedA4Width}\" ).");
            }
            else
            {
                // Width does not match; report the discrepancy and abort
                string message = $"Validation failed: Page width is {pageWidthInches:F2}\" but expected {expectedA4Width}\".";
                Console.Error.WriteLine(message);
                throw new Exception(message);
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime errors and report them
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}