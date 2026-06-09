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
            string inputPath = "input.vsdx";

            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the text set for the left side of the header
            string leftHeaderText = diagram.HeaderFooter.HeaderLeft;

            // Display the retrieved header text
            Console.WriteLine("Left Header: " + leftHeaderText);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
