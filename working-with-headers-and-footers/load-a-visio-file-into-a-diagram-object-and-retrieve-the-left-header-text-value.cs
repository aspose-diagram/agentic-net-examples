using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string filePath = "sample.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Retrieve the left header text
            string leftHeader = diagram.HeaderFooter.HeaderLeft;

            // Display the header text
            Console.WriteLine($"Left Header: {leftHeader}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
