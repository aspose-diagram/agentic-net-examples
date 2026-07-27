using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaModuleExtractor
{
    // Extracts the VBA code of a specified module from a Visio diagram and saves it to a .bas file.
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path, module name, output .bas file path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: VbaModuleExtractor <inputDiagram> <moduleName> <outputBasFile>");
            return;
        }

        string diagramPath = args[0];
        string moduleName = args[1];
        string outputBasPath = args[2];

        // Load the Visio diagram
        Diagram diagram = new Diagram(diagramPath);

        // Ensure the diagram contains a VBA project
        VbaProject vbaProject = diagram.VbaProject;
        if (vbaProject == null)
        {
            Console.WriteLine("The diagram does not contain a VBA project.");
            return;
        }

        // Retrieve the requested VBA module by name
        VbaModule vbaModule = vbaProject.Modules[moduleName];
        if (vbaModule == null)
        {
            Console.WriteLine($"VBA module '{moduleName}' not found in the diagram.");
            return;
        }

        // Get the VBA source code from the module
        string vbaCode = vbaModule.Codes;

        // Write the code to the specified .bas file
        try
        {
            File.WriteAllText(outputBasPath, vbaCode);
            Console.WriteLine($"VBA module '{moduleName}' has been saved to '{outputBasPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write the .bas file: {ex.Message}");
        }
    }
}
