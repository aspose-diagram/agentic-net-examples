using System;
using System.IO;
using Aspose.Diagram;

class VbaCloneExample
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram (contains VBA) and the target diagram
            string sourceDiagramPath = "source.vsdx";
            string targetDiagramPath = "target.vsdx";
            string outputDiagramPath = "target_with_vba.vsdx";

            // Load the source diagram (contains the VBA project)
            Diagram sourceDiagram = new Diagram(sourceDiagramPath);

            // Load the target diagram (will receive the VBA project)
            Diagram targetDiagram = new Diagram(targetDiagramPath);

            // Clone the VBA project data from source to target
            // VbProjectData holds the VBA project in MIME‑encoded byte array form
            byte[] vbaProjectData = sourceDiagram.VbProjectData;
            targetDiagram.VbProjectData = vbaProjectData;

            // Save the modified target diagram
            targetDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            // Clean up resources
            sourceDiagram.Dispose();
            targetDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
