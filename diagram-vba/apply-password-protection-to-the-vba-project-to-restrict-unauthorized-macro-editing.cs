using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project (read‑only property)
            var vbaProject = diagram.VbaProject;

            // Add a new procedural VBA module
            int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "SecurityModule");
            var vbaModule = vbaProject.Modules[moduleIndex];

            // Insert VBA code into the module
            vbaModule.Codes = @"
            Attribute VB_Name = ""SecurityModule""
            Sub ProtectedMacro()
            MsgBox ""This macro is protected.""
            End Sub
            ";

            // NOTE:
            // Aspose.Diagram does not expose an API to set a password on the VBA project.
            // Password protection must be applied manually in Visio after saving,
            // or by using a different tool that can modify the VBA project password.

            // Save the diagram in a macro‑enabled format to preserve the VBA code
            diagram.Save("output.vsdm", SaveFileFormat.Vsdm);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
