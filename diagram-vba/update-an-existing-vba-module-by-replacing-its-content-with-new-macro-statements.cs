using System;
using System.IO;
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

            // Retrieve the VBA module you want to update (replace "Module1" with the actual module name)
            VbaModule module = vbaProject.Modules["Module1"];

            // New macro code to replace the existing content
            string newMacro = @"
            Sub NewMacro()
            MsgBox ""Hello from new macro!""
            End Sub
            ";

            // Replace the module's code with the new macro statements
            module.Codes = newMacro;

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
