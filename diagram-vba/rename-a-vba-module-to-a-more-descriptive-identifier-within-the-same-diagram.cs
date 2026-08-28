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

            // Retrieve the module you want to rename (replace "OldModuleName" with the actual name)
            VbaModule module = vbaProject.Modules["OldModuleName"];

            // Assign a new, more descriptive name to the module
            module.Name = "DescriptiveModuleName";

            // Save the diagram with the updated VBA module name
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
