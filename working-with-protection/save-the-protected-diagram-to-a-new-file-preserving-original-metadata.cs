using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the protected source diagram
            string sourcePath = "protected_input.vsdx";

            // Path for the new diagram file (metadata will be preserved)
            string destinationPath = "protected_output.vsdx";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourcePath);

            // Create save options for the same format (VSDX) to keep original metadata
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);

            // Save the diagram to the new file using the specified options
            diagram.Save(destinationPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
