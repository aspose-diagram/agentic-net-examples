using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Convert and save the diagram to PNG format
            diagram.Save("output.png", SaveFileFormat.Png);

            // Dispose the Diagram object to promptly release unmanaged resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
