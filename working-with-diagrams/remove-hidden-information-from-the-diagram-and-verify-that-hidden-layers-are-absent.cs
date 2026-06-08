using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RemoveHiddenInfoExample
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Check if the diagram contains any hidden information
            bool hasHiddenBefore = diagram.HasHiddenInfo();
            Console.WriteLine($"Has hidden information before removal: {hasHiddenBefore}");

            // Remove all types of hidden information (PersonalInfo, Shapes, Masters, Styles, DataRecordSets)
            // The enum values are not marked with [Flags], so we combine them manually by summing their integer values.
            int allHiddenInfo = (int)RemoveHiddenInfoItem.PersonalInfo |
                                (int)RemoveHiddenInfoItem.Shapes |
                                (int)RemoveHiddenInfoItem.Masters |
                                (int)RemoveHiddenInfoItem.Styles |
                                (int)RemoveHiddenInfoItem.DataRecordSets;

            diagram.RemoveHiddenInformation(allHiddenInfo);

            // Verify that hidden information has been removed
            bool hasHiddenAfter = diagram.HasHiddenInfo();
            Console.WriteLine($"Has hidden information after removal: {hasHiddenAfter}");

            // Save the cleaned diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Dispose the diagram object
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
