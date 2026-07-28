using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramDisclaimerBatch
{
    // Standard disclaimer text to be added to each diagram
    private const string DisclaimerText = "Disclaimer: This diagram is confidential and intended for internal use only.";

    // Coordinates where the comment will be placed on each page
    private const double CommentPinX = 0.5;
    private const double CommentPinY = 0.5;

    static void Main()
    {
        try
        {

            // Path to the root folder containing Visio diagrams
            string projectDirectory = @"C:\Path\To\Project";

            // Retrieve all Visio files (common extensions) recursively
            var diagramFiles = Directory.GetFiles(projectDirectory, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".vsdx", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".vsd", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".vdx", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            foreach (var filePath in diagramFiles)
            {
                // Load the diagram using the provided constructor
                using (var diagram = new Diagram(filePath))
                {
                    // Add the disclaimer comment to every page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        page.AddComment(CommentPinX, CommentPinY, DisclaimerText);
                    }

                    // Prepare save options preserving the original file format
                    var saveOptions = new DiagramSaveOptions();
                    if (filePath.EndsWith(".vsdx", StringComparison.OrdinalIgnoreCase))
                        saveOptions.SaveFormat = SaveFileFormat.Vsdx;
                    else if (filePath.EndsWith(".vsd", StringComparison.OrdinalIgnoreCase))
                        saveOptions.SaveFormat = SaveFileFormat.Vsd;
                    else
                        saveOptions.SaveFormat = SaveFileFormat.Vdx;

                    // Save the modified diagram back to the same file using the provided Save method
                    diagram.Save(filePath, saveOptions);
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
