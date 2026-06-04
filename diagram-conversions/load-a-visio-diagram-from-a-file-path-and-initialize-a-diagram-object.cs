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
            string filePath = @"C:\Diagrams\sample.vsdx";

            // Load the diagram from the specified file path
            Diagram diagram = new Diagram(filePath);

            // The diagram object is now initialized and ready for further processing
            Console.WriteLine("Diagram loaded successfully. Pages count: " + diagram.Pages.Count);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
