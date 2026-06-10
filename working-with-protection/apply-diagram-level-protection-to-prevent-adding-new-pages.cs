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

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply global document protection to restrict modifications, including adding new pages
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;   // Prevent changes to background pages
            diagram.DocumentSettings.ProtectMasters = BOOL.True; // Prevent creation/editing of masters
            diagram.DocumentSettings.ProtectShapes = BOOL.True;  // Prevent selection of locked shapes
            diagram.DocumentSettings.ProtectStyles = BOOL.True;  // Prevent creation/editing of styles

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
