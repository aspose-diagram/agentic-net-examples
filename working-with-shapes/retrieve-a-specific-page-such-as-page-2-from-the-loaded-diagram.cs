using System;
using System.IO;
using Aspose.Diagram;

class RetrievePageExample
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (assumes a load rule is defined elsewhere)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the page named "Page-2" from the diagram's page collection
            Page targetPage = diagram.Pages.GetPage("Page-2");

            // Example usage: output the page ID and name
            System.Console.WriteLine($"Retrieved page ID: {targetPage.ID}, Name: {targetPage.Name}");

            // Dispose the diagram when done
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
