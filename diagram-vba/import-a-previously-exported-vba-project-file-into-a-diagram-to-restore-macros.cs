using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths – adjust as needed
            string diagramPath = "inputDiagram.vsdx";   // Existing Visio file
            string vbaFilePath = "exportedProject.vba"; // Exported VBA project file (plain text)
            string outputPath = "outputDiagram.vsdm";   // Macro‑enabled Visio file

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read the VBA project file content
            if (!File.Exists(vbaFilePath))
            {
                Console.WriteLine($"VBA file not found: {vbaFilePath}");
                return;
            }
            string vbaCode = File.ReadAllText(vbaFilePath);

            // Add a new procedural module and set its code
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "ImportedModule");
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];
            module.Codes = vbaCode;

            // Save the diagram in a macro‑enabled format to preserve the VBA project
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"Diagram saved with imported VBA macros to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
