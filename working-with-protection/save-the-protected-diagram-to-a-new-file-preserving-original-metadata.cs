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

            // Path for the new copy that will preserve all original metadata
            string destinationPath = "preserved_output.vsdx";

            // Load the diagram from the source file
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Create save options for the same format as the source (VSDX)
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);

                // Save the diagram to the new file, preserving metadata
                diagram.Save(destinationPath, saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
