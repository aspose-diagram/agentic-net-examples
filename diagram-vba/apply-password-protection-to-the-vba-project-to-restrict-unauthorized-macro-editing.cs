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

            // Path to the source Visio file (must contain VBA macros)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;

            // ------------------------------------------------------------
            // NOTE:
            // Aspose.Diagram does not expose a direct API to set a password
            // on the VBA project. If a password property becomes available
            // in a future version, it can be set here, e.g.:
            // vbaProject.Password = "MySecretPassword";
            // ------------------------------------------------------------

            // Save the diagram in a macro‑enabled format to preserve VBA
            string outputPath = "output.vsdm";
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine("Diagram saved as macro‑enabled file: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
