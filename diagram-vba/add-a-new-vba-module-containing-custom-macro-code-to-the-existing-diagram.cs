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

            // Path to the existing Visio diagram
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Define the VBA module name and the macro code to insert
            string moduleName = "MyMacroModule";
            string macroCode = @"
            Sub MyMacro()
            MsgBox ""Hello from VBA macro!""
            End Sub
            ";

            // Add a new procedural VBA module to the diagram's VBA project
            // The Add method returns the index of the newly added module
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, moduleName);

            // Retrieve the added module using the returned index
            VbaModule vbaModule = diagram.VbaProject.Modules[moduleIndex];

            // Set the macro code for the module
            vbaModule.Codes = macroCode;

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
