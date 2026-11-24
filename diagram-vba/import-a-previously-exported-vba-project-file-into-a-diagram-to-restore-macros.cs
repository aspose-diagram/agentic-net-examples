using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, VBA project file path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <vbaProjectPath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string vbaProjectPath = args[1];
        string outputDiagramPath = args[2];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: verify VBA project file exists
        if (!File.Exists(vbaProjectPath))
        {
            Console.Error.WriteLine($"VBA project file not found: {vbaProjectPath}");
            return;
        }

        try
        {
            // Load the existing diagram (macro-enabled formats such as .vsdm are supported)
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Ensure the diagram has a VBA project (required for macro-enabled files)
                if (diagram.VbaProject == null)
                {
                    Console.Error.WriteLine("The loaded diagram does not contain a VBA project.");
                    return;
                }

                // Read the exported VBA project file (assumed to be plain text containing VBA code)
                string vbaCode = File.ReadAllText(vbaProjectPath);

                // Add a new VBA module to the diagram and set its code
                // VbaModuleType.Procedural indicates a standard code module
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "ImportedModule");
                VbaModule module = diagram.VbaProject.Modules[moduleIndex];
                module.Codes = vbaCode;

                // Save the diagram in a macro-enabled format to preserve the imported macros
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved with imported VBA macros to: {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}