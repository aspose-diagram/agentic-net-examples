using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, VBA module name, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <vbaModuleName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string moduleName = args[1];
        string outputPath = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // New VBA macro statements to replace the existing module content
        // In a real scenario, you might read this from a file or another source.
        string newVbaCode = @"
Attribute VB_Name = ""{0}""
Sub NewMacro()
    MsgBox ""This is the updated macro.""
End Sub
".Replace("{0}", moduleName);

        try
        {
            // Load the existing Visio diagram (supports macro-enabled formats)
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;

            // Find the module by name
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
                Console.Error.WriteLine($"Error: VBA module \"{moduleName}\" not found in the diagram.");
                return;
            }

            // Replace the module's code with the new macro statements
            targetModule.Codes = newVbaCode;

            // Save the diagram in a macro-enabled format to preserve VBA code
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"Successfully updated VBA module \"{moduleName}\" and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}