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

            // Load the diagram into memory using the constructor that specifies the file format
            Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx);

            // Example usage: output the number of pages in the loaded diagram
            Console.WriteLine($"Diagram loaded. Page count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
