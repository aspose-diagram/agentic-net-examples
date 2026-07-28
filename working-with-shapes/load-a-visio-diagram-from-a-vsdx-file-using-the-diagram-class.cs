using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file to load
            string inputPath = "example.vsdx";

            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Simple verification output
            Console.WriteLine($"Diagram loaded successfully. Number of pages: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
