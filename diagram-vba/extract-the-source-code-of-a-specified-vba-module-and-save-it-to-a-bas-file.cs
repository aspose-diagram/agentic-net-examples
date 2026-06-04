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

            // Path to the source Visio diagram file
            string diagramPath = "input.vsdx";

            // Name of the VBA module to extract
            string moduleName = "Module1";

            // Destination path for the extracted .bas file
            string outputBasPath = "Module1.bas";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Retrieve the specified VBA module by its name
            VbaModule vbaModule = vbaProject.Modules[moduleName];

            // Get the VBA source code from the module
            string vbaCode = vbaModule.Codes;

            // Write the source code to a .bas file
            File.WriteAllText(outputBasPath, vbaCode);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
