using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the original diagram file
            Diagram diagram = new Diagram("original.vsdx");

            // (Optional) Perform any updates to the diagram here

            // Save the updated diagram to a new file, preserving the original unchanged
            diagram.Save("updated.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
