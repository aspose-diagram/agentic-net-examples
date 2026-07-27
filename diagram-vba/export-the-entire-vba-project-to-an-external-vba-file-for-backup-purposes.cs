using System;
using System.IO;
using Aspose.Diagram;

class ExportVbaProject
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the VBA project data (MIME‑encoded) as a byte array
            byte[] vbaData = diagram.VbProjectData;

            // Write the VBA project to an external .vba file for backup
            File.WriteAllBytes("backup.vba", vbaData);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
