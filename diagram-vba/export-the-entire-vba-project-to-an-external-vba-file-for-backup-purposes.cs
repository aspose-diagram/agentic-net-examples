using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class ExportVbaProject
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsd");

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Define the output .vba file path
            string outputFile = "VbaBackup.vba";

            // Write all VBA modules to the .vba file
            using (StreamWriter writer = new StreamWriter(outputFile, false))
            {
                foreach (VbaModule module in vbaProject.Modules)
                {
                    // Write a header for each module
                    writer.WriteLine("'-------------------------------------------------");
                    writer.WriteLine($"' Module Name: {module.Name}");
                    writer.WriteLine("'-------------------------------------------------");

                    // Write the actual VBA code of the module
                    writer.WriteLine(module.Codes);
                    writer.WriteLine(); // Add an empty line between modules
                }
            }

            Console.WriteLine($"VBA project successfully exported to '{outputFile}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
