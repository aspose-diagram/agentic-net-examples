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

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Check if a VBA project and modules exist
            if (diagram.VbaProject != null && diagram.VbaProject.Modules != null)
            {
                // Enumerate and list each VBA module name
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    Console.WriteLine(module.Name);
                }
            }
            else
            {
                Console.WriteLine("No VBA project or modules found.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
