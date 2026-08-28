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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply read‑only protection to the entire document
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                diagram.DocumentSettings.ProtectShapes = BOOL.True;
                diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // Note: Aspose.Diagram does not support assigning a password for editing restrictions.
                // The protection flags above make the document read‑only in Visio.

                // Save the protected diagram
                string outputPath = "output_protected.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
