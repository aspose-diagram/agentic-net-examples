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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project (read‑only property)
            var vbaProject = diagram.VbaProject;

            // Display whether the VBA project is already signed
            Console.WriteLine($"VBA project signed: {vbaProject.IsSigned}");

            // Add a new procedural VBA module named "MyModule"
            int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "MyModule");

            // Retrieve the newly added module
            var module = vbaProject.Modules[moduleIndex];

            // Set the VBA source code for the module
            module.Codes = @"
            Attribute VB_Name = ""MyModule""
            Sub HelloWorld()
            MsgBox ""Hello from Aspose.Diagram!""
            End Sub
            ";

            // Save the diagram in a macro‑enabled format to preserve the VBA project
            string outputPath = "output.vsdm";
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"Diagram saved with VBA module to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
