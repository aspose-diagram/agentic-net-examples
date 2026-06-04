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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Verify that a VBA project exists in the diagram
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Aspose.Diagram does not expose an API to set a password on the VBA project.
            // As a workaround, add a VBA module that can contain custom code.
            // The actual password protection must be applied manually in Visio.
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "ProtectionModule");
            var vbaModule = diagram.VbaProject.Modules[moduleIndex];
            vbaModule.Codes = @"
            Sub AutoOpen()
            ' Placeholder: VBA project password protection must be set manually in Visio.
            End Sub
            ";

            // Save the diagram in a macro‑enabled format to preserve the VBA project
            diagram.Save("output.vsdm", SaveFileFormat.Vsdm);
            Console.WriteLine("Diagram saved with a placeholder VBA module.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
