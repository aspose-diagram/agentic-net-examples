using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram file, VBA module name, output .bas file
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: ExtractVbaModule <inputDiagram> <moduleName> <outputBas>");
            return;
        }

        string diagramPath = args[0];
        string moduleName = args[1];
        string outputPath = args[2];

        // Load the Visio diagram
        Diagram diagram = new Diagram(diagramPath);

        // Verify that the diagram contains a VBA project
        if (diagram.VbaProject == null)
        {
            Console.WriteLine("The diagram does not contain a VBA project.");
            return;
        }

        // Retrieve the specified VBA module by name
        VbaModule vbaModule = diagram.VbaProject.Modules[moduleName];
        if (vbaModule == null)
        {
            Console.WriteLine($"VBA module '{moduleName}' not found in the diagram.");
            return;
        }

        // Extract the VBA source code from the module
        string vbaCode = vbaModule.Codes;

        // Write the source code to a .bas file
        File.WriteAllText(outputPath, vbaCode);

        Console.WriteLine($"VBA module '{moduleName}' has been extracted to '{outputPath}'.");
    }
}
