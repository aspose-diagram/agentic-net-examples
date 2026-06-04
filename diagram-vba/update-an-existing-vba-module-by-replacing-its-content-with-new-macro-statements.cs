using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class UpdateVbaModule
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project within the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Specify the name of the module to update
            string targetModuleName = "MyMacroModule";

            // Retrieve the module by name; if it does not exist, add a new procedural module
            VbaModule vbaModule;
            if (vbaProject.Modules[targetModuleName] != null)
            {
                vbaModule = vbaProject.Modules[targetModuleName];
            }
            else
            {
                // Add a new procedural module with the desired name
                int index = vbaProject.Modules.Add(VbaModuleType.Procedural, targetModuleName);
                vbaModule = vbaProject.Modules[index];
            }

            // Replace the existing code with new macro statements
            vbaModule.Codes = @"
            Sub NewMacro()
            MsgBox ""Hello from the updated macro!""
            End Sub
            ";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
