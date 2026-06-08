using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source VSDX file
            string inputPath = "input.vsdx";

            // Path where the cleaned VSDX file will be saved
            string outputPath = "output_cleaned.vsdx";

            // Load the diagram from the VSDX file
            Diagram diagram = new Diagram(inputPath);

            // Remove any VBA macros that may be embedded in the diagram
            diagram.RemoveMacro();

            // Build a mask that includes all hidden information categories to be removed
            int hiddenInfoMask = (int)(
                RemoveHiddenInfoItem.PersonalInfo |
                RemoveHiddenInfoItem.Shapes |
                RemoveHiddenInfoItem.Masters |
                RemoveHiddenInfoItem.Styles |
                RemoveHiddenInfoItem.DataRecordSets);

            // Remove the hidden information based on the mask
            diagram.RemoveHiddenInformation(hiddenInfoMask);

            // Save the cleaned diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
