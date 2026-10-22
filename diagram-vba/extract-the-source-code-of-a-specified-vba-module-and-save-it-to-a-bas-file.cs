using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, VBA module name, output .bas file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <vbaModuleName> <outputBasPath>");
            return;
        }

        string visioPath = args[0];
        string moduleName = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Find the specified VBA module by name
            VbaModule targetModule = null;
            foreach (VbaModule module in vbaProject.Modules)
            {
                if (string.Equals(module.Name, moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    targetModule = module;
                    break;
                }
            }

            if (targetModule == null)
            {
                Console.Error.WriteLine($"VBA module \"{moduleName}\" not found in the diagram.");
                return;
            }

            // Extract the VBA source code
            string vbaCode = targetModule.Codes ?? string.Empty;

            // Write the code to the .bas file (overwrite if exists)
            File.WriteAllText(outputPath, vbaCode);
            Console.WriteLine($"VBA module \"{moduleName}\" extracted successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}