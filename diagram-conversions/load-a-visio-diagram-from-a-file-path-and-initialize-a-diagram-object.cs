using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = @"C:\Path\To\YourDiagram.vsdx";

            // Load the Visio diagram and initialize the Diagram object
            Diagram diagram = new Diagram(filePath);

            // The 'diagram' object is now ready for further processing.

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
