using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Save the diagram to a different format (VDX in this example)
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

            // Release unmanaged resources held by the Diagram instance
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
