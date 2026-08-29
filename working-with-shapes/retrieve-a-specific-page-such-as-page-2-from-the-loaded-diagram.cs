using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the page named "Page-2"
            Page page = diagram.Pages.GetPage("Page-2");

            // Example usage: display page ID and name
            Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
