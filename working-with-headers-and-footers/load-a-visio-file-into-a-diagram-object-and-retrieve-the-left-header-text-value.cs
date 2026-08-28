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
            string filePath = "example.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Retrieve the left header text
            string leftHeader = diagram.HeaderFooter.HeaderLeft;

            // Output the retrieved value
            Console.WriteLine($"Left Header Text: {leftHeader}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
