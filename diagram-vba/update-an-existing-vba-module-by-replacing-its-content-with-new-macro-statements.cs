using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (must be macro-enabled to contain VBA)
            string inputPath = "input.vsdm";
            // Output Visio file where the VBA module will be updated
            string outputPath = "output.vsdm";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            var vbaProject = diagram.VbaProject;

            // Name of the module to update (or create if it does not exist)
            string targetModuleName = "MyMacroModule";

            // Locate the module index; -1 means not found
            int moduleIndex = -1;
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                var mod = vbaProject.Modules[i];
                if (mod.Name.Equals(targetModuleName, StringComparison.OrdinalIgnoreCase))
                {
                    moduleIndex = i;
                    break;
                }
            }

            // If the module does not exist, add a new procedural module
            if (moduleIndex == -1)
            {
                moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, targetModuleName);
            }

            // Retrieve the module
            var module = vbaProject.Modules[moduleIndex];

            // New VBA code to replace the existing content
            string newVbaCode = @"
            Attribute VB_Name = ""MyMacroModule""
            Sub HelloWorld()
            MsgBox ""Hello from Aspose.Diagram!""
            End Sub
            ";

            // Replace the module's code
            module.Codes = newVbaCode;

            // Save the diagram using a macro-enabled format to preserve VBA
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
