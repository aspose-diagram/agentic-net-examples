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

            // Load the diagram file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Enumerate all VBA modules and output their names
            foreach (VbaModule vbaModule in vbaProject.Modules)
            {
                Console.WriteLine(vbaModule.Name);
            }

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
