using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Choose the page you want to inspect (e.g., the first page)
            var page = diagram.Pages[0];

            // Access the PageProps of the selected page
            var pageProps = page.PageSheet.PageProps;

            // Read the PageWidth property (DoubleValue) and get its numeric value
            double pageWidth = pageProps.PageWidth.Value;

            // Output the page width
            Console.WriteLine($"Page width: {pageWidth}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
