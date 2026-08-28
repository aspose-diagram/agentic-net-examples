using System;
using System.IO;
using Aspose.Diagram;

class ImportVbaProject
{
    static void Main()
    {
        try
        {

            // Paths to the diagram, the exported VBA project file, and the output diagram
            string diagramPath = "inputDiagram.vsdx";
            string vbaProjectPath = "exportedMacro.vba";
            string outputDiagramPath = "outputDiagram.vsdx";

            // Load the existing Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Read the VBA project file (binary data)
                byte[] vbaData = File.ReadAllBytes(vbaProjectPath);

                // Assign the VBA data to the diagram to restore macros
                diagram.VbProjectData = vbaData;

                // Save the diagram with the imported VBA project
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
