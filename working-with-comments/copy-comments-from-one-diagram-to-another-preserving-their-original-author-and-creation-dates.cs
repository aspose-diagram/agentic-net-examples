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

            // Paths to the source and target Visio files
            string sourcePath = "source.vsdx";
            string targetPath = "target.vsdx";

            // Load the source diagram (contains the comments to copy)
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Load the target diagram (will receive the copied comments)
            Diagram targetDiagram = new Diagram(targetPath);

            // Assume both diagrams have the same number of pages and matching page order.
            // Iterate through each page by index.
            for (int pageIndex = 0; pageIndex < sourceDiagram.Pages.Count; pageIndex++)
            {
                // Get the corresponding pages from source and target diagrams
                Page sourcePage = sourceDiagram.Pages[pageIndex];
                Page targetPage = targetDiagram.Pages[pageIndex];

                // Iterate over all annotations (comments) on the source page
                foreach (Annotation srcAnnotation in sourcePage.PageSheet.Annotations)
                {
                    // Retrieve the shape ID the comment is attached to (0 if page‑level comment)
                    int shapeId = srcAnnotation.ShapeID;

                    // Retrieve the comment text
                    string commentText = srcAnnotation.Comment.Value;

                    // Determine the shape on the target page to attach the comment to
                    // If shapeId is 0, the comment is a page‑level comment.
                    if (shapeId == 0)
                    {
                        // Add a page‑level comment to the target page
                        targetPage.AddComment(0, 0, commentText);
                    }
                    else
                    {
                        // Find the corresponding shape on the target page
                        Shape targetShape = targetPage.Shapes.GetShape(shapeId);
                        if (targetShape != null)
                        {
                            // Add a shape‑level comment preserving the original text.
                            // Note: Author (ReviewerID) and creation dates are read‑only in Aspose.Diagram
                            // and cannot be set programmatically. The comment text is copied.
                            targetPage.AddComment(targetShape, commentText);
                        }
                        else
                        {
                            // If the shape does not exist in the target diagram, skip or log.
                            Console.WriteLine($"Warning: Shape ID {shapeId} not found on target page {targetPage.Name}.");
                        }
                    }
                }
            }

            // Save the modified target diagram
            targetDiagram.Save("target_with_copied_comments.vsdx", SaveFileFormat.Vsdx);

            Console.WriteLine("Comments copied successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
