using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (any supported format)
            Diagram diagram = new Diagram("input.vsdx");

            // Save the diagram to a new VSDX file.
            // The Save method with SaveFileFormat preserves the original page order.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
