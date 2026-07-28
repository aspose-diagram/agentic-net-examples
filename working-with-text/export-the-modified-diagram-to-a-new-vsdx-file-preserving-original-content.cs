using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the diagram that has been modified earlier in the workflow
            string inputPath = "input.vsdx";

            // Desired path for the exported copy preserving all original content
            string outputPath = "output_modified.vsdx";

            // Load the existing Visio file using the appropriate LoadFileFormat enum
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Export the diagram to a new .vsdx file. The Save method requires a
            // SaveFileFormat enum value as the second argument.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
