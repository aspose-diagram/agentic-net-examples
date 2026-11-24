using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect file path and expected width.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramFilePath> <expectedPageWidthInInches>");
            return;
        }

        string diagramPath = args[0];
        string expectedWidthStr = args[1];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse expected width.
        if (!double.TryParse(expectedWidthStr, out double expectedWidth))
        {
            Console.Error.WriteLine($"Error: Unable to parse expected width '{expectedWidthStr}' as a double.");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram does not contain any pages.");
            }

            // Access the first page's width.
            Page firstPage = diagram.Pages[0];
            double actualWidth = firstPage.PageSheet.PageProps.PageWidth.Value;

            // Compare actual width with expected width (tolerance of 0.001 inches).
            const double tolerance = 0.001;
            if (Math.Abs(actualWidth - expectedWidth) > tolerance)
            {
                throw new Exception($"PageWidth mismatch. Expected: {expectedWidth} inches, Actual: {actualWidth} inches.");
            }

            // If we reach here, the test passed.
            Console.WriteLine($"Success: PageWidth is {actualWidth} inches as expected.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}