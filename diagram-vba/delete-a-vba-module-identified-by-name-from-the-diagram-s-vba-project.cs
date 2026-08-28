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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the VBA module to delete
            string moduleName = "MyModule";

            // Access the collection of VBA modules
            VbaModuleCollection modules = diagram.VbaProject.Modules;

            // Remove the module if it exists
            if (modules[moduleName] != null)
            {
                modules.Remove(moduleName);
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
