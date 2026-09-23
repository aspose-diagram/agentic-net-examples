using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the sample Visio file (ensure the file exists at this location)
            string filePath = "sample.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Get the page height (value is in inches)
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Expected height for the known sample file (in inches)
            double expectedHeight = 11.0;

            // Verify the height matches the expected value
            if (Math.Abs(pageHeight - expectedHeight) > 0.001)
            {
                throw new Exception($"PageHeight test failed. Expected: {expectedHeight}, Actual: {pageHeight}");
            }

            Console.WriteLine($"PageHeight test passed. Height: {pageHeight} inches");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
