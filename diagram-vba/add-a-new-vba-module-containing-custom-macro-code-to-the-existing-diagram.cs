using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class AddVbaModuleExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Add a new procedural VBA module named "MyMacro" (uses VbaModuleCollection.Add)
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "MyMacro");

            // Retrieve the newly added module
            VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];

            // Set the VBA code for the module
            vbaModule.Codes = @"
            Sub HelloWorld()
            MsgBox ""Hello, World!""
            End Sub
            ";

            // Save the diagram with the new VBA module (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
