using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "sample.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // The 'diagram' object is now ready for further processing
            // Example: access the active page
            Page activePage = diagram.ActivePage;

            // (Additional processing can be added here)

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
