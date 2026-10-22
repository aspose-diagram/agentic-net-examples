using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioFile> <existingModuleName> <newModuleName>");
            return;
        }

        string inputPath = args[0];
        string existingModuleName = args[1];
        string newModuleName = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the VBA module with the specified existing name.
            int targetIndex = -1;
            for (int i = 0; i < diagram.VbaProject.Modules.Count; i++)
            {
                VbaModule module = diagram.VbaProject.Modules[i];
                if (module.Name.Equals(existingModuleName, StringComparison.OrdinalIgnoreCase))
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex == -1)
            {
                Console.Error.WriteLine($"Error: VBA module \"{existingModuleName}\" not found.");
                return;
            }

            // Rename the module.
            VbaModule targetModule = diagram.VbaProject.Modules[targetIndex];
            targetModule.Name = newModuleName;
            Console.WriteLine($"Module \"{existingModuleName}\" renamed to \"{newModuleName}\".");

            // Prepare output path – same directory with a suffix to preserve the original file.
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_renamed.vsdm");

            // Save the diagram in a macro‑enabled format to retain VBA code.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}