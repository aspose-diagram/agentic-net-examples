using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <moduleName> [outputBasPath]");
            return;
        }

        string inputPath = args[0];
        string moduleName = args[1];
        string outputPath = args.Length >= 3 ? args[2] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", moduleName + ".bas");

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains a VBA project.
                if (diagram.VbaProject == null)
                {
                    Console.Error.WriteLine("Error: The diagram does not contain a VBA project.");
                    return;
                }

                // Search for the specified VBA module by name.
                VbaModule targetModule = null;
                for (int i = 0; i < diagram.VbaProject.Modules.Count; i++)
                {
                    VbaModule mod = diagram.VbaProject.Modules[i];
                    if (string.Equals(mod.Name, moduleName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetModule = mod;
                        break;
                    }
                }

                if (targetModule == null)
                {
                    Console.Error.WriteLine($"Error: VBA module \"{moduleName}\" not found in the diagram.");
                    return;
                }

                // Extract the VBA source code.
                string vbaCode = targetModule.Codes ?? string.Empty;

                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the code to a .bas file.
                File.WriteAllText(outputPath, vbaCode);
                Console.WriteLine($"VBA module \"{moduleName}\" extracted successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}