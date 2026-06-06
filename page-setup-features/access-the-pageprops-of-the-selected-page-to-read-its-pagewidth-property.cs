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
            Diagram diagram = new Diagram(@"C:\Path\To\YourDiagram.vsdx");

            // Select the page you want to inspect (e.g., the first page)
            int pageIndex = 0; // change as needed
            Page selectedPage = diagram.Pages[pageIndex];

            // Access the PageProps of the selected page
            PageProps props = selectedPage.PageSheet.PageProps;

            // Read the PageWidth property (value is stored in a DoubleValue)
            double pageWidth = props.PageWidth.Value;

            // Output the page width
            Console.WriteLine($"Page Width (drawing units): {pageWidth}");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
