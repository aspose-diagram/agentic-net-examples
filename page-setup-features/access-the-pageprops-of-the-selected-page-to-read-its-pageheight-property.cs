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

            // Select the first page (or use diagram.Pages[index] for a specific page)
            Page page = diagram.Pages[0];

            // Access the PageProps of the selected page
            PageProps pageProps = page.PageSheet.PageProps;

            // Read the PageHeight property (value is stored in a DoubleValue)
            double pageHeight = pageProps.PageHeight.Value;

            // Output the page height
            Console.WriteLine($"Page Height: {pageHeight}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
