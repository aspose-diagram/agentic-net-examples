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

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Combine all hidden information flags
            int hiddenInfoFlags =
                (int)RemoveHiddenInfoItem.PersonalInfo |
                (int)RemoveHiddenInfoItem.Shapes |
                (int)RemoveHiddenInfoItem.Masters |
                (int)RemoveHiddenInfoItem.Styles |
                (int)RemoveHiddenInfoItem.DataRecordSets;

            // Remove hidden information based on the combined flags
            diagram.RemoveHiddenInformation(hiddenInfoFlags);

            // Verify that the diagram no longer contains hidden information
            bool hasHiddenInfo = diagram.HasHiddenInfo();
            Console.WriteLine("Has hidden information after removal: " + hasHiddenInfo);

            // Save the cleaned diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
