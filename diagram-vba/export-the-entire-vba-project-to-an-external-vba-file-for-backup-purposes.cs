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
            string inputVisioPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputVisioPath);

            // Retrieve the VBA project data (MIME‑encoded byte array)
            byte[] vbaData = diagram.VbProjectData;

            // Define the output file for the VBA backup
            string outputVbaPath = "backup.vba";

            // Write the VBA project data to the external .vba file
            File.WriteAllBytes(outputVbaPath, vbaData);

            Console.WriteLine($"VBA project exported successfully to '{outputVbaPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
