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
            string visioFilePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the Visio diagram into a Diagram object
            Diagram diagram = new Diagram(visioFilePath);

            // At this point the diagram is loaded and can be manipulated
            Console.WriteLine($"Diagram loaded. Pages count: {diagram.Pages.Count}");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
