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
            const string sourcePath = "input.vsdx";

            // Load the diagram using the constructor that accepts a file path
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Remove all types of hidden information
                int allHiddenItems = (int)RemoveHiddenInfoItem.PersonalInfo |
                                     (int)RemoveHiddenInfoItem.Shapes |
                                     (int)RemoveHiddenInfoItem.Masters |
                                     (int)RemoveHiddenInfoItem.Styles |
                                     (int)RemoveHiddenInfoItem.DataRecordSets;

                diagram.RemoveHiddenInformation(allHiddenItems);

                // Verify that the diagram reports no hidden information
                bool hasHiddenInfo = diagram.HasHiddenInfo();
                Console.WriteLine($"HasHiddenInfo after removal: {hasHiddenInfo}");

                // Inspect validation metadata for any remaining issues
                var issues = diagram.Validation.Issues;
                Console.WriteLine($"Number of validation issues: {issues.Count}");

                // If there are issues, list them (optional)
                foreach (var issue in issues)
                {
                    Console.WriteLine($"Issue: {issue}");
                }

                // Optionally save the cleaned diagram
                const string outputPath = "output_cleaned.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
