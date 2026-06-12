using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (provide the correct file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the page with the name "Page-2"
            Page targetPage = diagram.Pages.GetPage("Page-2");

            // Example usage: display the page's ID and name
            System.Console.WriteLine($"Page ID: {targetPage.ID}, Name: {targetPage.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
