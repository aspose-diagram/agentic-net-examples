using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Select the first page (or any page by index)
            Page page = diagram.Pages[0];

            // Access the PageProps of the selected page and read the PageWidth value
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

            // Output the page width
            Console.WriteLine($"Page width: {pageWidth}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
