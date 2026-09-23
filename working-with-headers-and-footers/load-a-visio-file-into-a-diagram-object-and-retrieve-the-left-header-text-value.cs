using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Retrieve the left header text
            string leftHeader = diagram.HeaderFooter.HeaderLeft;

            // Output the header text
            Console.WriteLine($"Left Header: {leftHeader}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
