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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply global protection to prevent theme changes.
            // Protecting styles disables modifications that affect the document theme.
            diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // (Optional) Additional protection to lock other aspects of the document.
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            diagram.DocumentSettings.ProtectMasters = BOOL.True;
            diagram.DocumentSettings.ProtectShapes = BOOL.True;

            // Save the protected diagram
            string outputPath = "output_protected.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
