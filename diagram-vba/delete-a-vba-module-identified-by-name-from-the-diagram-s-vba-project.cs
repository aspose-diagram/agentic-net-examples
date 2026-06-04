using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class DeleteVbaModule
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the VBA module to be removed
            string moduleName = "MyMacroModule";

            // Remove the specified VBA module from the diagram's VBA project
            diagram.VbaProject.Modules.Remove(moduleName);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
