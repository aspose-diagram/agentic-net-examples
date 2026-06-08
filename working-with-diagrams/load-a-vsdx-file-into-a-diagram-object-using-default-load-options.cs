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
            string filePath = "example.vsdx";

            // Load the diagram using the default load options (auto-detect format)
            Diagram diagram = new Diagram(filePath);

            // Example usage: output the number of pages in the loaded diagram
            Console.WriteLine("Number of pages: " + diagram.Pages.Count);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
