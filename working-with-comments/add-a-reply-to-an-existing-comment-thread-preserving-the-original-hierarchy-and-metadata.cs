using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Retrieve the first page (adjust index or name as needed)
            var page = diagram.Pages[0];

            // Find the first existing comment (annotation) on the page
            Annotation existingComment = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                existingComment = ann;
                break; // take the first comment as the thread starter
            }

            if (existingComment == null)
            {
                Console.WriteLine("No existing comments found on the page.");
                return;
            }

            // Preserve hierarchy metadata: use the same shape and reviewer as the original comment
            int shapeId = existingComment.ShapeID;          // shape to which the original comment is attached
            int reviewerId = existingComment.ReviewerID.Value; // reviewer identifier

            // Retrieve the shape instance by its ID
            Shape targetShape = page.Shapes[shapeId];

            // Add a reply comment to the same shape
            // The AddComment overload attaches the comment to the shape and creates a new annotation
            page.AddComment(targetShape, "This is a reply to the original comment.");

            // Optionally, update the reviewer of the new comment to match the original
            // The newly added annotation will be the last one in the collection
            Annotation replyComment = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                replyComment = ann; // iterate to the last annotation
            }

            if (replyComment != null)
            {
                // Set the reviewer ID to match the original comment's reviewer
                replyComment.ReviewerID.Value = reviewerId;
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            Console.WriteLine("Reply added and diagram saved as output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
