using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing diagram (preserves all pages and their order)
            Diagram diagram = new Diagram("input.vsdx");

            // Save the diagram to a new VSDX file; page order remains unchanged
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
