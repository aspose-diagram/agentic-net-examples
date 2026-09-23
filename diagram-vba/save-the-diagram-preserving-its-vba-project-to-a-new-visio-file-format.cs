using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (VBA project is loaded automatically)
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram to a new Visio file format (e.g., VSDX) preserving the VBA project
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
