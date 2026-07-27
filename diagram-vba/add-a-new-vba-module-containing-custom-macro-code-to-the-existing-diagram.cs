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

            // Optional: remove any existing VBA/macros
            diagram.RemoveMacro();

            // Add a new procedural VBA module named "CustomMacro"
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "CustomMacro");

            // Retrieve the newly added module
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];

            // Set the VBA code for the module
            module.Codes = @"Sub HelloWorld()
            MsgBox ""Hello from Aspose.Diagram!""
            End Sub";

            // Save the diagram with the new VBA module
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
