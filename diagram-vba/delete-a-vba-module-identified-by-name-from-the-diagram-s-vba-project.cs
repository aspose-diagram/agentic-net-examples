using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <moduleNameToDelete> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string moduleName = args[1];
        string outputPath = args.Length >= 3 ? args[2] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdm");

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains a VBA project.
            if (diagram.VbaProject == null)
            {
                Console.Error.WriteLine("Error: The diagram does not contain a VBA project.");
                return;
            }

            // Find the index of the module with the specified name.
            int moduleIndex = -1;
            for (int i = 0; i < diagram.VbaProject.Modules.Count; i++)
            {
                VbaModule mod = diagram.VbaProject.Modules[i];
                if (mod != null && string.Equals(mod.Name, moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    moduleIndex = i;
                    break;
                }
            }

            if (moduleIndex == -1)
            {
                Console.WriteLine($"Info: No VBA module named \"{moduleName}\" was found.");
            }
            else
            {
                // Delete the module at the identified index.
                diagram.VbaProject.Modules.RemoveAt(moduleIndex);
                Console.WriteLine($"Info: VBA module \"{moduleName}\" has been deleted.");
            }

            // Save the modified diagram in a macro‑enabled format.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Info: Modified diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}