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

            // Paths to the source and cleaned Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the VSDX file
            Diagram diagram = new Diagram(inputPath);

            // Combine all hidden‑information flags that should be removed
            int hiddenInfoMask = (int)RemoveHiddenInfoItem.PersonalInfo |
                                 (int)RemoveHiddenInfoItem.Shapes |
                                 (int)RemoveHiddenInfoItem.Masters |
                                 (int)RemoveHiddenInfoItem.Styles |
                                 (int)RemoveHiddenInfoItem.DataRecordSets;

            // Remove hidden metadata from the diagram
            diagram.RemoveHiddenInformation(hiddenInfoMask);

            // Remove any VBA/macros that may be present
            diagram.RemoveMacro();

            // Save the cleaned diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
