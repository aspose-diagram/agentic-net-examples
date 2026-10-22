using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the sample Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <path-to-sample.vsdx>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram does not contain any pages.");
            }

            // Access the first page's height (in inches).
            double actualHeight = diagram.Pages[0].PageSheet.PageProps.PageHeight.Value;

            // TODO: Replace the placeholder with the expected page height for the known sample file.
            double expectedHeight = 0.0; // e.g., 11.0 for Letter size

            // Verify that the actual height matches the expected height within a small tolerance.
            const double tolerance = 0.001;
            if (Math.Abs(actualHeight - expectedHeight) > tolerance)
            {
                throw new Exception($"PageHeight verification failed. Expected: {expectedHeight}, Actual: {actualHeight}");
            }

            Console.WriteLine("PageHeight verification passed.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}