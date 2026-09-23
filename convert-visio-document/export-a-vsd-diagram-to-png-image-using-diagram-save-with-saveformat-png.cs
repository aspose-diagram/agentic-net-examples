using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSD diagram from file
            Diagram diagram = new Diagram("input.vsd");

            // Export the diagram to PNG format
            diagram.Save("output.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
