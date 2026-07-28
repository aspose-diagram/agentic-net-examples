using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RemoveHiddenInfoValidator
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Load the diagram using the built‑in constructor (load rule)
            Diagram diagram = new Diagram(sourcePath);

            // Remove all types of hidden information
            // Combine enum values using bitwise OR as the method expects an int flag
            int removeFlags = (int)RemoveHiddenInfoItem.PersonalInfo |
                              (int)RemoveHiddenInfoItem.Shapes |
                              (int)RemoveHiddenInfoItem.Masters |
                              (int)RemoveHiddenInfoItem.Styles |
                              (int)RemoveHiddenInfoItem.DataRecordSets;

            diagram.RemoveHiddenInformation(removeFlags);

            // Validate that no hidden information remains
            bool hasHiddenInfo = diagram.HasHiddenInfo();

            // Additionally, inspect validation issues (if any)
            var issues = diagram.Validation?.Issues;
            int issueCount = issues != null ? issues.Count : 0;

            // Output the validation result
            Console.WriteLine("HasHiddenInfo after removal: " + hasHiddenInfo);
            Console.WriteLine("Number of validation issues: " + issueCount);

            // Optionally, save the cleaned diagram (save rule)
            string outputPath = "output_cleaned.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
