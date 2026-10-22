using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, VBA project file path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <vbaProjectPath> <outputDiagramPath>");
            return;
        }

        string diagramPath = args[0];
        string vbaPath = args[1];
        string outputPath = args[2];

        // Guard: ensure input diagram exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: ensure VBA project file exists
        if (!File.Exists(vbaPath))
        {
            Console.Error.WriteLine($"Error: VBA project file not found at '{vbaPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read VBA project file content (assumed to be plain VBA source code)
            string vbaCode = File.ReadAllText(vbaPath);

            // Add a new procedural VBA module with the name derived from the file name
            string moduleName = Path.GetFileNameWithoutExtension(vbaPath);
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, moduleName);

            // Retrieve the newly added module and set its code
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];
            module.Codes = vbaCode;

            // Save the diagram in a macro‑enabled format to preserve the imported VBA code
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            // Dispose the diagram to free resources
            diagram.Dispose();

            Console.WriteLine($"Successfully imported VBA project and saved diagram to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}