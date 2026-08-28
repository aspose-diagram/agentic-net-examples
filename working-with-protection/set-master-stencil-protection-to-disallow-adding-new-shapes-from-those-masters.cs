using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Disallow adding new shapes from masters by enabling master protection
            diagram.DocumentSettings.ProtectMasters = BOOL.True;

            // Save the protected diagram to a new file
            string outputPath = "output_protected.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
