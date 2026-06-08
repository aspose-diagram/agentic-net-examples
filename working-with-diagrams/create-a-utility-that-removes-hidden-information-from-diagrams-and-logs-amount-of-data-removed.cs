using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramHiddenInfoCleaner
{
    // Removes hidden information and macros from a Visio diagram,
    // then saves the cleaned diagram and logs the actions performed.
    public static void Clean(string inputFilePath, string outputFilePath)
    {
        // Load the diagram from the specified file (lifecycle: load)
        using (Diagram diagram = new Diagram(inputFilePath))
        {
            // Check if the diagram contains hidden information before cleaning
            bool hadHiddenInfo = diagram.HasHiddenInfo();

            // Log initial state
            Console.WriteLine($"Diagram loaded: \"{inputFilePath}\"");
            Console.WriteLine($"Has hidden information before cleaning: {hadHiddenInfo}");

            // Define the items to remove (PersonalInfo | Shapes | Masters | Styles | DataRecordSets)
            int itemsToRemove = (int)(
                RemoveHiddenInfoItem.PersonalInfo |
                RemoveHiddenInfoItem.Shapes |
                RemoveHiddenInfoItem.Masters |
                RemoveHiddenInfoItem.Styles |
                RemoveHiddenInfoItem.DataRecordSets);

            // Remove hidden information (feature: RemoveHiddenInformation)
            diagram.RemoveHiddenInformation(itemsToRemove);
            Console.WriteLine("Removed hidden information (PersonalInfo, Shapes, Masters, Styles, DataRecordSets).");

            // Remove any VBA/macros present (feature: RemoveMacro)
            diagram.RemoveMacro();
            Console.WriteLine("Removed VBA/macros from the diagram.");

            // Verify hidden information after cleaning
            bool hasHiddenInfoAfter = diagram.HasHiddenInfo();
            Console.WriteLine($"Has hidden information after cleaning: {hasHiddenInfoAfter}");

            // Save the cleaned diagram to the output path (lifecycle: save)
            diagram.Save(outputFilePath, SaveFileFormat.Vdx);
            Console.WriteLine($"Cleaned diagram saved to: \"{outputFilePath}\"");
        }
    }
}

// Example usage:
// DiagramHiddenInfoCleaner.Clean("input.vsdx", "output_cleaned.vdx");

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramHiddenInfoCleaner.Clean("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
