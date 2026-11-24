using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath> <outputDiagramPath>");
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
            // Load source diagram
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Determine the number of pages to process (use the minimum count to avoid out‑of‑range errors)
                    int pageCount = Math.Min(sourceDiagram.Pages.Count, targetDiagram.Pages.Count);

                    for (int i = 0; i < pageCount; i++)
                    {
                        Page srcPage = sourceDiagram.Pages[i];
                        Page tgtPage = targetDiagram.Pages[i];

                        // Iterate over each annotation (comment) on the source page
                        foreach (Annotation srcAnn in srcPage.PageSheet.Annotations)
                        {
                            // Retrieve comment text (fallback to empty string if null)
                            string commentText = srcAnn.Comment?.Value ?? string.Empty;

                            // Retrieve position (fallback to 0.0 if null)
                            double posX = srcAnn.X?.Value ?? 0.0;
                            double posY = srcAnn.Y?.Value ?? 0.0;

                            // Add a new comment to the target page at the same coordinates
                            tgtPage.AddComment(posX, posY, commentText);

                            // The newly added annotation is the last one in the collection
                            if (tgtPage.PageSheet.Annotations.Count > 0)
                            {
                                Annotation newAnn = tgtPage.PageSheet.Annotations[tgtPage.PageSheet.Annotations.Count - 1];

                                // Preserve the shape association if any (ShapeID is writable)
                                newAnn.ShapeID = srcAnn.ShapeID;

                                // NOTE: ReviewerID and Date properties are read‑only in the Aspose.Diagram API.
                                // Therefore they cannot be set on the newly created annotation.
                                // // TODO: If future API versions expose setters for ReviewerID and Date,
                                // // assign them here to fully preserve author and creation date.
                            }
                        }
                    }

                    // Save the modified target diagram to the specified output path
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Comments copied successfully. Output saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}