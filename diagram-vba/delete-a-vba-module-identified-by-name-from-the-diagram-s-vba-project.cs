using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input file, module name to delete, and output file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <moduleNameToDelete> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string moduleName = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Check if the diagram contains a VBA project.
            if (diagram.VbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project. No modules to delete.");
                return;
            }

            // Flag to indicate whether the module was found.
            bool moduleRemoved = false;

            // Iterate over the modules collection using an index-based loop.
            // This allows safe removal via RemoveAt when the target module is found.
            for (int i = 0; i < diagram.VbaProject.Modules.Count; i++)
            {
                VbaModule module = diagram.VbaProject.Modules[i];
                if (module != null && string.Equals(module.Name, moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    // Remove the module at the identified index.
                    diagram.VbaProject.Modules.RemoveAt(i);
                    moduleRemoved = true;
                    Console.WriteLine($"Module \"{moduleName}\" has been removed.");
                    break;
                }
            }

            if (!moduleRemoved)
            {
                Console.Error.WriteLine($"Module \"{moduleName}\" was not found in the VBA project.");
                return;
            }

            // Prepare save options for a macro-enabled Visio format.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdm);

            // Save the modified diagram.
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O exceptions and report them.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}