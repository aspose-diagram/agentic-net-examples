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

            // Load the diagram using the appropriate load format
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Enable protection on masters to prevent adding new shapes from those masters
            diagram.DocumentSettings.ProtectMasters = BOOL.True;

            // Save the protected diagram
            string outputPath = "output_protected.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Master stencil protection applied and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
