using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (preserves all shapes, ActiveX controls, and layout)
            Diagram diagram = new Diagram("input.vsdx");

            // No modifications are required; loading retains all ActiveX control configurations and layout

            // Save the diagram to a new file while preserving the original content
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
