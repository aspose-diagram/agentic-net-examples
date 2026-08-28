using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the known sample Visio file.
            string filePath = "sample.vsdx";

            // Expected page width in inches for the sample file.
            double expectedWidth = 8.5;
            const double tolerance = 0.001; // Allowable difference due to rounding.

            // Load the diagram using the Aspose.Diagram constructor.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram does not contain any pages.");

                // Retrieve the first page.
                Page page = diagram.Pages[0];

                // Read the page width (in inches) from the page's PageProps.
                double actualWidth = page.PageSheet.PageProps.PageWidth.Value;

                // Verify the width matches the expected value within tolerance.
                if (Math.Abs(actualWidth - expectedWidth) > tolerance)
                    throw new Exception($"Page width mismatch. Expected {expectedWidth}, but got {actualWidth}.");

                // If no exception was thrown, the test passes.
                Console.WriteLine($"Page width test passed. Width = {actualWidth}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
