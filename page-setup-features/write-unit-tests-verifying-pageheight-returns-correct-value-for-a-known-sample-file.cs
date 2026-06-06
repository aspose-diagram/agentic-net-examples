using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the known sample Visio file
            string filePath = "sample.vsdx";

            // Load the diagram using the Aspose.Diagram constructor
            using (Diagram diagram = new Diagram(filePath))
            {
                // Access the first page in the document
                Page page = diagram.Pages[0];

                // Retrieve the page height (in inches)
                double actualHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Expected height for the sample file (adjust as needed for the actual file)
                double expectedHeight = 11.0;

                // Verify the height; throw an exception on failure, otherwise write success message
                if (Math.Abs(actualHeight - expectedHeight) > 0.001)
                {
                    throw new Exception($"PageHeight test failed. Expected {expectedHeight}, but got {actualHeight}.");
                }
                else
                {
                    Console.WriteLine("PageHeight test passed.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
