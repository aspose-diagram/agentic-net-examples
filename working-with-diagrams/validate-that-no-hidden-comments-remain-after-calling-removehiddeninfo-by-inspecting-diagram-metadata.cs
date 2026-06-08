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

            // Paths to the source and destination Visio files
            string sourcePath = "input.vsdx";
            string destinationPath = "output.vsdx";

            // Load the diagram using the provided constructor (load rule)
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Combine all hidden‑info items to be removed
                int itemsToRemove =
                    (int)(RemoveHiddenInfoItem.PersonalInfo |
                          RemoveHiddenInfoItem.Shapes |
                          RemoveHiddenInfoItem.Masters |
                          RemoveHiddenInfoItem.Styles |
                          RemoveHiddenInfoItem.DataRecordSets);

                // Remove hidden information (remove‑hidden‑info rule)
                diagram.RemoveHiddenInformation(itemsToRemove);

                // Validate that no hidden information remains
                if (diagram.HasHiddenInfo())
                {
                    throw new InvalidOperationException("Hidden information still exists after removal.");
                }

                // Optional: inspect validation issues for any residual hidden comments
                // (The Issue class details are not required for this validation step)
                foreach (var issue in diagram.Validation.Issues)
                {
                    // If needed, add custom checks on issue properties here
                }

                // Save the cleaned diagram using the provided save rule
                diagram.Save(destinationPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
