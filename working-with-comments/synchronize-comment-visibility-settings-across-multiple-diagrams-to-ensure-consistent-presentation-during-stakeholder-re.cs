using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Define the file paths: first diagram is the source, the rest are targets.
            string[] diagramPaths = new string[]
            {
                "SourceDiagram.vsdx",
                "TargetDiagram1.vsdx",
                "TargetDiagram2.vsdx"
            };

            // Load the source diagram.
            Diagram sourceDiagram = new Diagram(diagramPaths[0]);

            // Collect all comments (annotations) from the source diagram.
            var sourceComments = new List<CommentInfo>();

            foreach (Page srcPage in sourceDiagram.Pages)
            {
                foreach (Annotation ann in srcPage.PageSheet.Annotations)
                {
                    sourceComments.Add(new CommentInfo
                    {
                        PageName = srcPage.Name,
                        ShapeId = ann.ShapeID,
                        Text = ann.Comment.Value,
                        ReviewerId = ann.ReviewerID.Value
                    });
                }
            }

            // Iterate over each target diagram and synchronize its comments.
            for (int i = 1; i < diagramPaths.Length; i++)
            {
                string targetPath = diagramPaths[i];
                Diagram targetDiagram = new Diagram(targetPath);

                foreach (Page tgtPage in targetDiagram.Pages)
                {
                    // Process comments that belong to the current page.
                    foreach (var srcComment in sourceComments)
                    {
                        if (srcComment.PageName != tgtPage.Name)
                            continue;

                        // Look for an existing annotation with the same ShapeID.
                        Annotation existing = null;
                        foreach (Annotation ann in tgtPage.PageSheet.Annotations)
                        {
                            if (ann.ShapeID == srcComment.ShapeId)
                            {
                                existing = ann;
                                break;
                            }
                        }

                        if (existing != null)
                        {
                            // Update the comment text and reviewer identifier.
                            existing.Comment.Value = srcComment.Text;
                            existing.ReviewerID.Value = srcComment.ReviewerId;
                        }
                        else
                        {
                            // No matching comment; add a new page‑level comment.
                            // Position (0,0) is a placeholder; adjust as needed.
                            tgtPage.AddComment(0, 0, srcComment.Text);
                        }
                    }
                }

                // Save the synchronized diagram with a new filename.
                string outputPath = Path.GetFileNameWithoutExtension(targetPath) + "_Synced.vsdx";
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Synchronized comments saved to: {outputPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Simple DTO to hold comment information from the source diagram.
    private class CommentInfo
    {
        public string PageName { get; set; }
        public int ShapeId { get; set; }
        public string Text { get; set; }
        public int ReviewerId { get; set; }
    }
}