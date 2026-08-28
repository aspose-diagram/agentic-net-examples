using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a VSD file
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram as a PNG image
            diagram.Save("output.png", SaveFileFormat.Png);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
