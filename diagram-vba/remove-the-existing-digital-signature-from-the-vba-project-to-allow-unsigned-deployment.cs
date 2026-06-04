using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file (can be .vsdx, .vdx, etc.)
            string inputPath = "input.vsdx";

            // Output file must be a macro‑enabled format to preserve VBA (even after removal)
            string outputPath = "output.vsdm";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that a VBA project exists and is signed
            if (diagram.VbaProject != null && diagram.VbaProject.IsSigned)
            {
                // Remove the VBA project data (clears the digital signature)
                diagram.VbProjectData = null;
                Console.WriteLine("Digital signature removed from VBA project.");
            }
            else
            {
                Console.WriteLine("No signed VBA project found; no action needed.");
            }

            // Save the diagram using a macro‑enabled format
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
