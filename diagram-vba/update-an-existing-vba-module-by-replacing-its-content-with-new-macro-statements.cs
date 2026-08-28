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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project within the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Name of the VBA module to be updated
            string targetModuleName = "Module1";

            // Locate the module by name
            VbaModule module = null;
            foreach (VbaModule mod in vbaProject.Modules)
            {
                if (mod.Name.Equals(targetModuleName, StringComparison.OrdinalIgnoreCase))
                {
                    module = mod;
                    break;
                }
            }

            // If the module exists, replace its code; otherwise add a new procedural module
            if (module != null)
            {
                // Replace the existing VBA code with new macro statements
                module.Codes = @"
            Sub NewMacro()
            MsgBox ""Hello from new macro!""
            End Sub
            ";
            }
            else
            {
                // Add a new procedural module with the specified name
                int index = vbaProject.Modules.Add(VbaModuleType.Procedural, targetModuleName);
                module = vbaProject.Modules[index];

                // Set the VBA code for the newly added module
                module.Codes = @"
            Sub NewMacro()
            MsgBox ""Hello from new macro!""
            End Sub
            ";
            }

            // Save the updated diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
