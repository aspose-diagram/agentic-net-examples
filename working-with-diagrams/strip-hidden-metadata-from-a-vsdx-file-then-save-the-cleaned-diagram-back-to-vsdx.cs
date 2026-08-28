using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CleanVisioDiagram
{
    static void Main()
    {
        try
        {

            // Paths to the source and cleaned Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_cleaned.vsdx";

            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // If the diagram contains hidden information, remove it
            if (diagram.HasHiddenInfo())
            {
                // Combine all hidden‑info flags that should be removed
                int allHiddenInfoItems =
                    (int)RemoveHiddenInfoItem.PersonalInfo |
                    (int)RemoveHiddenInfoItem.Shapes |
                    (int)RemoveHiddenInfoItem.Masters |
                    (int)RemoveHiddenInfoItem.Styles |
                    (int)RemoveHiddenInfoItem.DataRecordSets;

                diagram.RemoveHiddenInformation(allHiddenInfoItems);
            }

            // Remove any VBA/macros that may be embedded in the diagram
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
