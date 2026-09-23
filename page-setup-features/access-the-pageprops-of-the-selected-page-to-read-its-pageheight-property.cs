using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Select the page you want to work with.
                // Here we use the first page (index 0). Adjust as needed.
                Page page = diagram.Pages[0];

                // Read the page height (value is in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Output the page height
                Console.WriteLine($"Page Height: {pageHeight} inches");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
