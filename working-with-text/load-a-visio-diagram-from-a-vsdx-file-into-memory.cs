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

            // Load the Visio diagram into memory
            Diagram diagram = new Diagram(filePath);

            // Example usage: output the number of pages in the diagram
            Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
