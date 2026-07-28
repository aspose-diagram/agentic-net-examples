using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class HiddenInfoCleaner
{
    // Removes hidden information and macros from a Visio diagram,
    // then saves the cleaned diagram to a new file.
    public void Clean(string inputFilePath, string outputFilePath)
    {
        // Load the diagram from the specified file.
        using (Diagram diagram = new Diagram(inputFilePath))
        {
            // Check if the diagram contains hidden information before cleaning.
            bool hasHiddenInfoBefore = diagram.HasHiddenInfo();
            Console.WriteLine($"Hidden information present before cleaning: {hasHiddenInfoBefore}");

            // Remove all categories of hidden information.
            int allHiddenInfoItems =
                (int)RemoveHiddenInfoItem.PersonalInfo |
                (int)RemoveHiddenInfoItem.Shapes |
                (int)RemoveHiddenInfoItem.Masters |
                (int)RemoveHiddenInfoItem.Styles |
                (int)RemoveHiddenInfoItem.DataRecordSets;

            diagram.RemoveHiddenInformation(allHiddenInfoItems);
            Console.WriteLine("Removed hidden information categories: PersonalInfo, Shapes, Masters, Styles, DataRecordSets.");

            // Remove any VBA/macros that may be embedded.
            diagram.RemoveMacro();
            Console.WriteLine("Removed VBA/macros from the diagram.");

            // Check if hidden information still exists after cleaning.
            bool hasHiddenInfoAfter = diagram.HasHiddenInfo();
            Console.WriteLine($"Hidden information present after cleaning: {hasHiddenInfoAfter}");

            // Save the cleaned diagram using the same format as the source.
            // SaveFileFormat.Vdx is used as a common Visio format; adjust if needed.
            diagram.Save(outputFilePath, SaveFileFormat.Vdx);
            Console.WriteLine($"Cleaned diagram saved to: {outputFilePath}");
        }
    }
}

// Example usage:
// var cleaner = new HiddenInfoCleaner();
// cleaner.Clean("input.vsdx", "output.vsdx");

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new HiddenInfoCleaner();
            obj.Clean("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
