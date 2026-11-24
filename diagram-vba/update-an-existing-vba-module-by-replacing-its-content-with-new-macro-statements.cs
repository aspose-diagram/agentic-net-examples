using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: inputVisioPath, moduleName, outputVisioPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <moduleName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string targetModuleName = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram (macro-enabled formats such as .vsdm are supported)
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Locate the target module by name
            int moduleIndex = -1;
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                VbaModule module = vbaProject.Modules[i];
                if (string.Equals(module.Name, targetModuleName, StringComparison.OrdinalIgnoreCase))
                {
                    moduleIndex = i;
                    break;
                }
            }

            if (moduleIndex == -1)
            {
                Console.Error.WriteLine($"VBA module '{targetModuleName}' not found in the diagram.");
                return;
            }

            // Replace the module's code with new macro statements
            VbaModule targetModule = vbaProject.Modules[moduleIndex];
            string newMacroCode = @"
Attribute VB_Name = ""{0}""
Option Explicit

Sub NewMacro()
    MsgBox ""This is the new macro code.""
End Sub
".Trim();

            // Insert the actual module name into the attribute line
            targetModule.Codes = string.Format(newMacroCode, targetModule.Name);

            // Save the updated diagram using a macro-enabled format to preserve VBA
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdm);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Successfully updated VBA module '{targetModuleName}' and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}