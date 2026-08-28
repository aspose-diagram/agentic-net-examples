using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class HiddenInfoCleaner
{
    // Removes hidden information and macros from a Visio diagram,
    // then saves the cleaned file and logs what was removed.
    public static void Clean(string inputFilePath, string outputFilePath)
    {
        // Load the diagram using the provided constructor (lifecycle rule)
        using (Diagram diagram = new Diagram(inputFilePath))
        {
            // Check if the diagram contains any hidden information
            bool hasHiddenInfo = diagram.HasHiddenInfo();
            Console.WriteLine($"Diagram has hidden information: {hasHiddenInfo}");

            // Remove all types of hidden information (PersonalInfo, Shapes, Masters, Styles, DataRecordSets)
            // Combine enum values using bitwise OR (1|2|4|8|16 = 31)
            int allHiddenInfoFlags = (int)(
                RemoveHiddenInfoItem.PersonalInfo |
                RemoveHiddenInfoItem.Shapes |
                RemoveHiddenInfoItem.Masters |
                RemoveHiddenInfoItem.Styles |
                RemoveHiddenInfoItem.DataRecordSets);

            diagram.RemoveHiddenInformation(allHiddenInfoFlags);
            Console.WriteLine("Removed hidden information (PersonalInfo, Shapes, Masters, Styles, DataRecordSets).");

            // Remove any VBA/macros present in the diagram
            diagram.RemoveMacro();
            Console.WriteLine("Removed VBA/macros from the diagram.");

            // Verify removal
            bool stillHasHiddenInfo = diagram.HasHiddenInfo();
            Console.WriteLine($"Diagram still has hidden information after cleanup: {stillHasHiddenInfo}");

            // Save the cleaned diagram using the provided Save method (lifecycle rule)
            diagram.Save(outputFilePath, SaveFileFormat.Vdx);
            Console.WriteLine($"Cleaned diagram saved to: {outputFilePath}");
        }
    }
}

// Example usage:
// HiddenInfoCleaner.Clean("input.vsdx", "output_cleaned.vdx");

class Program
{
    static void Main(string[] args)
    {
        try
        {

            HiddenInfoCleaner.Clean("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
