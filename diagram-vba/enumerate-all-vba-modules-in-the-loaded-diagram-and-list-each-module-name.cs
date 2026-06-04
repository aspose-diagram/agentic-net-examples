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

            // Load the diagram (replace with the actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Get the collection of VBA modules
            VbaModuleCollection modules = vbaProject.Modules;

            // Enumerate each module and output its name
            foreach (VbaModule module in modules)
            {
                Console.WriteLine(module.Name);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
