using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (.vsdx)
            string visioFilePath = "example.vsdx";

            // Load the diagram into memory using the constructor that accepts a file name and format
            Diagram diagram = new Diagram(visioFilePath, LoadFileFormat.Vsdx);

            // At this point the diagram is loaded and can be manipulated in memory
            Console.WriteLine($"Diagram loaded. Pages count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
