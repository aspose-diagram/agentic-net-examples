using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, current module name, new module name
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <oldModuleName> <newModuleName>");
            return;
        }

        string inputPath = args[0];
        string oldModuleName = args[1];
        string newModuleName = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the VBA project
                VbaProject vbaProject = diagram.VbaProject;

                // Find the index of the module with the specified old name
                int moduleIndex = -1;
                for (int i = 0; i < vbaProject.Modules.Count; i++)
                {
                    VbaModule module = vbaProject.Modules[i];
                    if (module.Name.Equals(oldModuleName, StringComparison.OrdinalIgnoreCase))
                    {
                        moduleIndex = i;
                        break;
                    }
                }

                if (moduleIndex == -1)
                {
                    Console.Error.WriteLine($"Error: VBA module \"{oldModuleName}\" not found.");
                    return;
                }

                // Rename the module
                VbaModule targetModule = vbaProject.Modules[moduleIndex];
                targetModule.Name = newModuleName;

                // Prepare output path (same folder, suffix "_renamed")
                string directory = Path.GetDirectoryName(inputPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_renamed.vsdm");

                // Save the diagram in a macro‑enabled format to preserve VBA code
                diagram.Save(outputPath, SaveFileFormat.Vsdm);

                Console.WriteLine($"Module renamed successfully. Saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}