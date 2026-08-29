using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file to be loaded
            string visioFilePath = "example.vsdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(visioFilePath);

            // The diagram is now loaded and can be used for further processing
            Console.WriteLine($"Diagram loaded. Pages count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
