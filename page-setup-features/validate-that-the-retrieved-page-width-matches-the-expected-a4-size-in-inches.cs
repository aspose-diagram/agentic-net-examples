using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be validated
            string filePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the page width in inches
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                // Expected A4 width in inches
                const double expectedA4Width = 8.27;

                // Tolerance for floating‑point comparison
                const double tolerance = 0.01;

                // Validate the width
                if (Math.Abs(pageWidth - expectedA4Width) > tolerance)
                {
                    throw new Exception($"Page width {pageWidth} inches does not match expected A4 width {expectedA4Width} inches.");
                }
                else
                {
                    Console.WriteLine($"Page width validation passed: {pageWidth} inches.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
