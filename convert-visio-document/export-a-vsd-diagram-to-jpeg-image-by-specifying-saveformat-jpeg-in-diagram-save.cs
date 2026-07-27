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

            // Save the diagram as a JPEG image
            diagram.Save("output.jpg", SaveFileFormat.Jpeg);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
