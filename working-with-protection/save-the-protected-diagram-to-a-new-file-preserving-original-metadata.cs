using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the protected diagram file
            Diagram diagram = new Diagram("protected_diagram.vsdx");

            // Save it to a new file; original metadata is retained automatically
            diagram.Save("protected_diagram_copy.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
