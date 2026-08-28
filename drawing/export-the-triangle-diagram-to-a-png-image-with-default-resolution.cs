using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportTriangleDiagram
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (triangle.vsd)
            Diagram diagram = new Diagram("triangle.vsd");

            // Export the diagram to a PNG image using default resolution.
            // The file extension determines the output format.
            diagram.Save("triangle.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
