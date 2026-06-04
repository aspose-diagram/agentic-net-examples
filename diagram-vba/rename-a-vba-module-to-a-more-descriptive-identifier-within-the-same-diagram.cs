using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("inputDiagram.vsdx");

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Retrieve the VBA module you want to rename (by its current name)
            VbaModule module = vbaProject.Modules["OldModuleName"]; // replace with actual current name

            // Assign a new, more descriptive name to the module
            module.Name = "NewDescriptiveModuleName"; // replace with desired name

            // Save the modified diagram back to file
            diagram.Save("outputDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
