using System.IO;
using System;
using Aspose.Diagram;

class VbaProjectCloneExample
{
    static void Main()
    {
        try
        {

            // Load the source diagram that contains the VBA project
            Diagram sourceDiagram = new Diagram("sourceDiagram.vsdx");

            // Load the target diagram to which the VBA project will be attached
            Diagram targetDiagram = new Diagram("targetDiagram.vsdx");

            // Clone the VBA project data (MIME‑encoded byte array) from source to target
            targetDiagram.VbProjectData = sourceDiagram.VbProjectData;

            // Save the modified target diagram
            targetDiagram.Save("targetDiagram_WithVba.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
