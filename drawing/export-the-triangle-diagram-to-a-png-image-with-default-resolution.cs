using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram that contains the triangle shape
            Diagram diagram = new Diagram("triangle.vsdx");

            // Export the diagram to a PNG image using the default resolution
            diagram.Save("triangle.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
