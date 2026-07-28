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
            // Path for the protected output file
            string outputPath = "output_protected.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply read‑only protection to the entire document.
                // This prevents editing of shapes, masters, backgrounds and styles.
                diagram.DocumentSettings.ProtectShapes = BOOL.True;
                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // NOTE: Aspose.Diagram does not provide a property to set an edit‑password.
                // The protection flags above enforce read‑only behavior, but a password
                // cannot be assigned via the current API.

                // Save the protected diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram has been saved with read‑only protection.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
