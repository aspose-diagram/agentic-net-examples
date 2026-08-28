using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the original and cleaned Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_cleaned.vsdx";

            // Get original file size in bytes
            long originalSize = new FileInfo(inputPath).Length;

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Combine all hidden info items to be removed
            int removeMask = (int)(RemoveHiddenInfoItem.PersonalInfo |
                                   RemoveHiddenInfoItem.Shapes |
                                   RemoveHiddenInfoItem.Masters |
                                   RemoveHiddenInfoItem.Styles |
                                   RemoveHiddenInfoItem.DataRecordSets);

            // Remove hidden information from the diagram
            diagram.RemoveHiddenInformation(removeMask);

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Get cleaned file size in bytes
            long cleanedSize = new FileInfo(outputPath).Length;

            // Output size comparison
            Console.WriteLine($"Original size: {originalSize} bytes");
            Console.WriteLine($"Cleaned size: {cleanedSize} bytes");
            Console.WriteLine($"Size reduced: {originalSize - cleanedSize} bytes");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
