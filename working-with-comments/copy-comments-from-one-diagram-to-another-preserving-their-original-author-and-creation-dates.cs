using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <source.vsdx> <target.vsdx> <output.vsdx>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram inside a using block
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram inside a using block
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Iterate over pages by index to keep correspondence
                    int pageCount = sourceDiagram.Pages.Count;
                    for (int i = 0; i < pageCount; i++)
                    {
                        // Get source and target pages (assume same order)
                        Page sourcePage = sourceDiagram.Pages[i];
                        Page targetPage = targetDiagram.Pages[i];

                        // Iterate through all annotations (comments) on the source page
                        foreach (Annotation srcAnn in sourcePage.PageSheet.Annotations)
                        {
                            // Retrieve comment text, reviewer ID and creation date
                            string commentText = srcAnn.Comment?.Value ?? string.Empty;
                            int reviewerId = srcAnn.ReviewerID?.Value ?? 0;
                            DateTime creationDate = srcAnn.Date?.Value ?? DateTime.MinValue;

                            // Add the comment to the target page at a default position (0,0)
                            // Note: AddComment takes X, Y coordinates and the comment string
                            targetPage.AddComment(0, 0, commentText);

                            // Retrieve the newly added annotation (assumed to be the last one)
                            Annotation newAnn = targetPage.PageSheet.Annotations[targetPage.PageSheet.Annotations.Count - 1];

                            // Preserve original reviewer ID and creation date
                            if (newAnn.ReviewerID != null)
                                newAnn.ReviewerID.Value = reviewerId;
                            if (newAnn.Date != null)
                                newAnn.Date.Value = creationDate;
                        }
                    }

                    // Save the modified target diagram to the specified output path
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine("Comments copied successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}