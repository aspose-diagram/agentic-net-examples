using System.IO;
using System;
using Aspose.Diagram;

class PreserveVbaExample
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (must contain a VBA project)
            string sourcePath = "input.vsdm";

            // Load the diagram from the file
            Diagram diagram = new Diagram(sourcePath);

            // Optional: verify that the VBA project is present
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("No VBA project found in the source file.");
            }
            else
            {
                Console.WriteLine("VBA project detected and will be preserved.");
            }

            // Path for the new Visio file (choose a format that supports macros, e.g., VSDM)
            string outputPath = "output.vsdm";

            // Save the diagram using the SaveFileFormat that supports macros.
            // This preserves the VBA project in the new file.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"Diagram saved to '{outputPath}' with VBA project preserved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
