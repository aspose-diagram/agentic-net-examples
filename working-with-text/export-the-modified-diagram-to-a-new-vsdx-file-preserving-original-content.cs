using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram from a .vsdx file
            Diagram diagram = new Diagram("input.vsdx");

            // -------------------------------------------------
            // Place any diagram modifications here.
            // For example, you could add shapes, change properties, etc.
            // -------------------------------------------------
            // (Modification code omitted for brevity)

            // Save the modified diagram to a new .vsdx file,
            // preserving all original content.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
