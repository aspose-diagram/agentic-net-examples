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

            // Enable protection for background pages (prevent editing/deletion)
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;

            // Optionally, protect other elements globally
            // diagram.DocumentSettings.ProtectMasters = BOOL.True;
            // diagram.DocumentSettings.ProtectShapes = BOOL.True;
            // diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // Save the protected diagram
            string outputPath = "output_protected.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Diagram saved with background protection applied.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
