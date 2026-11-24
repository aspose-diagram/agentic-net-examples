using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the sample Visio file. Adjust as needed.
        string diagramPath = "sample.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram does not contain any pages.");
            }

            // Retrieve the first page (index 0).
            Page firstPage = diagram.Pages[0];

            // Access the page height (in inches) via the PageProps property.
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

            // Expected height for the known sample file (in inches).
            // Update this value to match the actual height of your sample file.
            double expectedHeight = 11.0;

            // Verify that the retrieved height matches the expected value.
            // Allow a small tolerance for floating‑point comparisons.
            double tolerance = 0.001;
            if (Math.Abs(pageHeight - expectedHeight) > tolerance)
            {
                throw new Exception($"PageHeight test failed. Expected: {expectedHeight}, Actual: {pageHeight}");
            }

            // If we reach this point, the test passed.
            Console.WriteLine("PageHeight test passed.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}