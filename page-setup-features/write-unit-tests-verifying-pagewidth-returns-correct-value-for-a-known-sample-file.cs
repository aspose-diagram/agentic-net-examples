using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the sample Visio file. Adjust as needed.
        string samplePath = "sample.vsdx";
        if (!System.IO.File.Exists(samplePath))
        {
            Console.Error.WriteLine($"File not found: {samplePath}");
            return;
        }

        // Verify the file exists before proceeding.
        if (!File.Exists(samplePath))
        {
            Console.Error.WriteLine($"Error: File not found - {samplePath}");
            return;
        }

        // Expected page width in inches for the known sample file.
        // This value should match the page setup of the sample diagram.
        double expectedPageWidth = 8.5; // Example: Letter size width.

        try
        {
            // Load the diagram from the file.
            Diagram diagram = new Diagram(samplePath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            // Access the first page (index 0).
            Page page = diagram.Pages[0];

            // Retrieve the page width using the correct property path.
            double actualPageWidth = page.PageSheet.PageProps.PageWidth.Value;

            // Output the retrieved width for diagnostic purposes.
            Console.WriteLine($"Actual Page Width: {actualPageWidth} inches");

            // Compare the actual width with the expected value using a tolerance.
            double tolerance = 0.001; // Allow minor floating‑point differences.
            if (Math.Abs(actualPageWidth - expectedPageWidth) > tolerance)
            {
                throw new Exception($"Page width mismatch. Expected: {expectedPageWidth}, Actual: {actualPageWidth}");
            }

            // If we reach this point, the test passed.
            Console.WriteLine("Test passed: Page width matches the expected value.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during test execution: {ex.Message}");
        }
    }
}