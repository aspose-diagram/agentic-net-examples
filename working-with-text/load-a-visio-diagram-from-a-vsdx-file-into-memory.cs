using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio .vsdx file
            string filePath = "sample.vsdx";

            // Load the diagram from the file, explicitly specifying the VSDX format
            Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx);

            // The diagram is now loaded into memory and can be manipulated
            Console.WriteLine($"Diagram loaded. Page count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
