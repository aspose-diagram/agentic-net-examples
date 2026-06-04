using System;
using System.IO;
using Aspose.Diagram;

class ImportVbaProject
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio diagram (VSDX, VSD, etc.)
            string diagramPath = "inputDiagram.vsdx";

            // Path to the exported VBA project file (.vba)
            string vbaProjectPath = "exportedMacro.vba";

            // Path where the diagram with restored macros will be saved
            string outputDiagramPath = "outputDiagramWithMacro.vsdx";

            // Load the diagram using the appropriate constructor (load rule)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Read the VBA project file into a byte array
                byte[] vbaData = File.ReadAllBytes(vbaProjectPath);

                // Assign the VBA project data to the diagram (property rule)
                diagram.VbProjectData = vbaData;

                // Optionally, set the VBA project name (if required)
                // diagram.VbaProject.Name = "MyMacroProject";

                // Save the diagram with the restored macros (save rule)
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
