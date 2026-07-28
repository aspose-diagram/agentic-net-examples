using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file to be loaded
            string filePath = "sample.vsdx";

            // Load the diagram using the default constructor that applies default load options
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
