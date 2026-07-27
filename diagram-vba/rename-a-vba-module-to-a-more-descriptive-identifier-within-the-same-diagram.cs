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

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Define the current module name and the desired new name
            string oldModuleName = "OldModule";
            string newModuleName = "NewDescriptiveModule";

            // Retrieve the module by its current name
            VbaModule module = vbaProject.Modules[oldModuleName];

            // If the module exists, rename it
            if (module != null)
            {
                module.Name = newModuleName;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
