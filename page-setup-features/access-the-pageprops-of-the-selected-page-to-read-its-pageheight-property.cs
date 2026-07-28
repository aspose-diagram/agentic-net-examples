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
            Diagram diagram = new Diagram("input.vsdx");

            // Choose the page you want to inspect (e.g., the first page)
            Page page = diagram.Pages[0];

            // Access the PageProps of the selected page
            DoubleValue pageHeightValue = page.PageSheet.PageProps.PageHeight;

            // Retrieve the numeric height from the DoubleValue object
            double pageHeight = pageHeightValue.Value;

            // Display the page height
            Console.WriteLine($"Page Height: {pageHeight}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
