using System;
using System.IO;
using Aspose.Diagram;

class ImportVbaProject
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file (VSDX, VSD, etc.)
            string diagramPath = @"C:\Docs\MyDiagram.vsdx";

            // Path to the exported VBA project file (.vba)
            string vbaProjectPath = @"C:\Docs\MyMacroProject.vba";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read the VBA project data (MIME encoded) from the .vba file
            byte[] vbaData = File.ReadAllBytes(vbaProjectPath);

            // Assign the VBA project data to the diagram
            diagram.VbProjectData = vbaData;

            // Optionally set the VBA project name (if desired)
            // diagram.VbaProject.Name = "MyMacroProject";

            // Save the diagram with the imported macros
            string outputPath = @"C:\Docs\MyDiagram_WithMacros.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
