using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of annotations (comments) on the current page.
                    AnnotationCollection annotations = page.PageSheet.Annotations;

                    if (annotations == null || annotations.Count == 0)
                    {
                        // No comments on this page; continue to the next page.
                        continue;
                    }

                    // First pass: collect all comment (annotation) IDs.
                    List<int> commentIds = new List<int>();
                    foreach (Annotation ann in annotations)
                    {
                        // MarkerIndex uniquely identifies a comment.
                        if (ann.MarkerIndex != null)
                        {
                            commentIds.Add(ann.MarkerIndex.Value);
                        }
                    }

                    // Second pass: retrieve each comment by its ID and display details.
                    foreach (int commentId in commentIds)
                    {
                        Annotation found = null;

                        // Search the annotations collection for the matching ID.
                        foreach (Annotation ann in annotations)
                        {
                            if (ann.MarkerIndex != null && ann.MarkerIndex.Value == commentId)
                            {
                                found = ann;
                                break;
                            }
                        }

                        if (found != null)
                        {
                            // Extract comment details, handling possible null values.
                            string commentText = (found.Comment != null) ? found.Comment.Value : string.Empty;
                            int reviewerId = (found.ReviewerID != null) ? found.ReviewerID.Value : 0;
                            int shapeId = found.ShapeID; // ShapeID is a primitive int.
                            DateTime commentDate = DateTime.MinValue;
                            if (found.Date != null)
                            {
                                commentDate = found.Date.Value;
                            }

                            // Output the comment information to the console.
                            Console.WriteLine($"Page {page.ID}, Comment ID: {commentId}");
                            Console.WriteLine($"  Text      : {commentText}");
                            Console.WriteLine($"  ReviewerID: {reviewerId}");
                            Console.WriteLine($"  ShapeID   : {shapeId}");
                            Console.WriteLine($"  Date      : {commentDate}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}