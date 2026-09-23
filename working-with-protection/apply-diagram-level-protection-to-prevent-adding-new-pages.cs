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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply global protection to the diagram.
            // These protection flags restrict modifications and also prevent adding new pages.
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            diagram.DocumentSettings.ProtectMasters = BOOL.True;
            diagram.DocumentSettings.ProtectShapes = BOOL.True;
            diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // Save the protected diagram
            string outputPath = "protected_output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
