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

            // Load an existing Visio file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            var vbaProject = diagram.VbaProject;

            // Add a new procedural VBA module
            int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "TestModule");
            var module = vbaProject.Modules[moduleIndex];

            // Define the macro code
            string macroCode = @"
            Attribute VB_Name = ""TestModule""
            Sub TestMacro()
            MsgBox ""Hello from macro""
            End Sub
            ";

            // Set the macro code in the module
            module.Codes = macroCode;

            // Save the diagram in a macro‑enabled format
            string outputPath = "output.vsdm";
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            // Capture and display the macro code for verification
            Console.WriteLine("Macro code added to the document:");
            Console.WriteLine(module.Codes);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
