using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to load
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Select the first page (index 0)
                Page page = diagram.Pages[0];

                // Read the page width (value is in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                // Output the page width
                Console.WriteLine($"Page width: {pageWidth} inches");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
