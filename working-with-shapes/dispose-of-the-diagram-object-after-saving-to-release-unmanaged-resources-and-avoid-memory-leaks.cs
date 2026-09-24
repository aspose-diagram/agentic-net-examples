using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Save the diagram to a new file (or format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Dispose the Diagram object to free unmanaged resources and avoid memory leaks
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
