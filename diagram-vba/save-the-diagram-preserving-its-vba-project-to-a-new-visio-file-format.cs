using System.IO;
using System;
using Aspose.Diagram;

class PreserveVbaExample
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (can be any format that contains a VBA project)
            string sourcePath = "input.vsdm";

            // Path for the new Visio file where the VBA project will be preserved
            string outputPath = "output.vsdm";

            // Load the diagram from the source file
            // Uses the Diagram(string) constructor as defined in the provided rules
            Diagram diagram = new Diagram(sourcePath);

            // The VbaProject property is automatically retained when the diagram is saved,
            // so no additional handling is required.

            // Save the diagram to a macro‑enabled Visio format (VSDM) to keep the VBA project.
            // Uses the Save(string, SaveFileFormat) method from the rules.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Diagram saved with VBA project preserved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
