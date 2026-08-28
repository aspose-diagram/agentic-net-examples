using System.IO;
using System;
using Aspose.Diagram;

class VbaProjectCloner
{
    static void Main()
    {
        try
        {

            // Load the source diagram that contains the VBA project
            Diagram sourceDiagram = new Diagram("sourceDiagram.vsdx");

            // Retrieve the VBA project data (MIME encoded) from the source diagram
            byte[] vbaProjectData = sourceDiagram.VbProjectData;

            // Load the target diagram where the VBA project will be attached
            Diagram targetDiagram = new Diagram("targetDiagram.vsdx");

            // Assign the retrieved VBA project data to the target diagram
            targetDiagram.VbProjectData = vbaProjectData;

            // Save the modified target diagram (now containing the cloned VBA project)
            targetDiagram.Save("targetDiagram_WithVba.vsdx", SaveFileFormat.Vsdx);

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
