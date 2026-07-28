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

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(sourcePath);

                // Load the target diagram (or create a new empty one)
                Diagram targetDiagram = new Diagram(targetPath);

                // Iterate through each page in the source diagram
                foreach (Page sourcePage in sourceDiagram.Pages)
                {
                    // Try to find a page with the same name in the target diagram
                    Page targetPage = targetDiagram.Pages.GetPage(sourcePage.Name);
                    if (targetPage == null)
                    {
                        // If not found, create a new page in the target diagram
                        targetPage = new Page();
                        targetPage.Name = sourcePage.Name;
                        targetDiagram.Pages.Add(targetPage);
                    }

                    // Copy page‑level comments
                    foreach (Annotation srcAnnotation in sourcePage.PageSheet.Annotations)
                    {
                        // Only copy comments that are not attached to a shape (ShapeID == 0)
                        if (srcAnnotation.ShapeID == 0)
                        {
                            // The Annotation class does not expose X/Y coordinates,
                            // so we place the copied comment at (0,0) as a placeholder.
                            // The author (ReviewerID) and creation dates are read‑only
                            // and cannot be set via the API.
                            targetPage.AddComment(0.0, 0.0, srcAnnotation.Comment.Value);
                        }
                        else
                        {
                            // Shape‑level comment: try to locate the corresponding shape
                            // in the target page by the same shape ID.
                            // This works only if the target diagram already contains the
                            // shape with the same ID (e.g., after a Combine operation).
                            Shape srcShape = sourcePage.Shapes.GetShape(srcAnnotation.ShapeID);
                            Shape tgtShape = null;
                            try
                            {
                                tgtShape = targetPage.Shapes.GetShape(srcAnnotation.ShapeID);
                            }
                            catch
                            {
                                // Shape not found in target; skip this comment.
                            }

                            if (tgtShape != null)
                            {
                                // Add the comment to the target shape.
                                // Again, author and dates cannot be set.
                                targetPage.AddComment(tgtShape, srcAnnotation.Comment.Value);
                            }
                        }
                    }
                }

                // Save the modified target diagram
                targetDiagram.Save("target_with_copied_comments.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }