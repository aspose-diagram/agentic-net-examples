using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Name of the page to retrieve
            string targetPageName = "Page-1";

            // Retrieve the page by its name
            Page page = diagram.Pages.GetPage(targetPageName);

            // Example usage: display page information
            Console.WriteLine($"Retrieved page: {page.Name} (ID: {page.ID})");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
