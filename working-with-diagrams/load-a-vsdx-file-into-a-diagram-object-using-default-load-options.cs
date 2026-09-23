using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file to be loaded
            string filePath = "input.vsdx";

            // Load the VSDX file into a Diagram object using default load options
            Diagram diagram = new Diagram(filePath);

            // At this point the diagram object is ready for further processing
            Console.WriteLine("Diagram loaded successfully. Pages count: " + diagram.Pages.Count);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
