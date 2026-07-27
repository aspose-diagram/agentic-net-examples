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

            string inputPath = "input.vsdx";
            string outputPath = "output.vsdm";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            var vbaProject = diagram.VbaProject;

            // Add a new procedural module (or retrieve existing one)
            int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "ProtectionModule");
            var module = vbaProject.Modules[moduleIndex];

            // Set VBA code for the module
            module.Codes = @"
            Attribute VB_Name = ""ProtectionModule""
            Sub Dummy()
            MsgBox ""This is a protected macro.""
            End Sub
            ";

            // Note: Aspose.Diagram does not expose an API to set a password on the VBA project.
            // The VBA project will be saved in a macro-enabled file format.

            // Save the diagram as a macro-enabled Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine("Diagram saved with VBA module to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
