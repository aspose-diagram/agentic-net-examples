using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSD file
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram as PDF
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
