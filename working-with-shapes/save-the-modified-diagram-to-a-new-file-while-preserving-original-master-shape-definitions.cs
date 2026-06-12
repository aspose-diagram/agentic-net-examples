using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the original Visio diagram
            string sourcePath = "source.vsdx";
            Diagram diagram = new Diagram(sourcePath);

            // (Optional) perform modifications to the diagram here

            // Save the modified diagram to a new file, preserving master shapes
            string targetPath = "target.vsdx";
            diagram.Save(targetPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
