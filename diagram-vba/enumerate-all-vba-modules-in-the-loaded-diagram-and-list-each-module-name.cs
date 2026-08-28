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

            // Load the diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Enumerate all VBA modules and output their names
            foreach (VbaModule vbaModule in vbaProject.Modules)
            {
                Console.WriteLine(vbaModule.Name);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
