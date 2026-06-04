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

            // Paths to the input Visio file and the output macro‑enabled file
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdm";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the VBA project
                VbaProject vbaProject = diagram.VbaProject;

                // Display whether the VBA project is signed
                Console.WriteLine("VBA project signed: " + vbaProject.IsSigned);

                // Add a new procedural VBA module named "MyModule"
                int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "MyModule");

                // Retrieve the newly added module
                VbaModule module = vbaProject.Modules[moduleIndex];

                // Set the VBA code for the module
                module.Codes = @"Attribute VB_Name = ""MyModule""
            Sub HelloWorld()
            MsgBox ""Hello from Aspose.Diagram!""
            End Sub";

                // Save the diagram in a macro‑enabled format to preserve the VBA project
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
            }

            Console.WriteLine("Diagram saved with VBA module.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
