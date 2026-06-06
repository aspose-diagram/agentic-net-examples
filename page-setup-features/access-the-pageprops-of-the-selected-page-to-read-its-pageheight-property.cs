using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Access the first page (or change index as needed)
                Page page = diagram.Pages[0];

                // Read the page height (value is in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Output the page height
                Console.WriteLine($"Page height: {pageHeight} inches");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
