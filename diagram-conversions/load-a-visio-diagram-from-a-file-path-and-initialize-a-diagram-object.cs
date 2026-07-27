using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Specify the path to the Visio file to be loaded
            string filePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the Visio diagram from the file and initialize the Diagram object
            Diagram diagram = new Diagram(filePath);

            // The diagram is now ready for further processing
            // Example: access the active page
            Page activePage = diagram.ActivePage;
            Console.WriteLine($"Active page name: {activePage.Name}");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
