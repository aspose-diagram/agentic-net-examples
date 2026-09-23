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

            // Path to the Visio file (must be a macro‑enabled .vsdm or .vsdx with VBA)
            string filePath = "sample.vsdm";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;

            // Output basic VBA project information
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

            // The Aspose.Diagram.Vba API provides only IsSigned and Name.
            // Detailed signer information (e.g., signer name) is not exposed directly.
            // To obtain such details you would need to inspect the underlying digital
            // signature using external certificate APIs.

            // List VBA modules (optional, for completeness)
            foreach (VbaModule module in vbaProject.Modules)
            {
                Console.WriteLine($"Module: {module.Name}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
