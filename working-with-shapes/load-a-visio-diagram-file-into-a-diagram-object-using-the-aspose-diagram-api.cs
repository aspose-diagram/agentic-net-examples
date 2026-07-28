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
            string visioFilePath = "example.vsdx";

            // Load the Visio diagram into a Diagram object using the constructor that accepts a file name
            Diagram diagram = new Diagram(visioFilePath);

            // The diagram is now loaded and can be manipulated as needed
            Console.WriteLine($"Diagram loaded. Pages count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
