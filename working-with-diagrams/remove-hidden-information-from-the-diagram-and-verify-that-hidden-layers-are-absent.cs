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
            Diagram diagram = new Diagram("input.vsdx");

            // Combine all hidden‑info flags to remove everything that can be hidden
            int removeMask = (int)(
                RemoveHiddenInfoItem.PersonalInfo |
                RemoveHiddenInfoItem.Shapes |
                RemoveHiddenInfoItem.Masters |
                RemoveHiddenInfoItem.Styles |
                RemoveHiddenInfoItem.DataRecordSets);

            // Remove hidden information from the diagram
            diagram.RemoveHiddenInformation(removeMask);

            // Verify that no hidden information remains
            bool hasHiddenInfo = diagram.HasHiddenInfo();
            Console.WriteLine("Has hidden information after removal: " + hasHiddenInfo);

            // Save the cleaned diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
