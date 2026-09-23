using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio diagram
            string inputPath = "input.vsdx";
            // Path where the updated diagram will be saved
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Choose the page that contains the comment thread (first page in this example)
            Page page = diagram.Pages[0];

            // Identifier of the comment to which we want to reply
            // This should match the MarkerIndex of the existing annotation
            long targetMarkerId = 1; // <-- replace with actual comment ID

            // Locate the target annotation
            Annotation targetAnnotation = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                if (ann.MarkerIndex.Value == targetMarkerId)
                {
                    targetAnnotation = ann;
                    break;
                }
            }

            if (targetAnnotation == null)
            {
                Console.WriteLine($"Comment with MarkerIndex {targetMarkerId} not found.");
                return;
            }

            // Retrieve the shape associated with the original comment (if any)
            // ShapeID is stored directly on the annotation
            Shape associatedShape = null;
            try
            {
                associatedShape = page.Shapes.GetShape(targetAnnotation.ShapeID);
            }
            catch
            {
                // If the shape cannot be found, we will add a page‑level comment instead
            }

            // Prepare the reply text
            string replyText = $"Reply to comment #{targetMarkerId}: {targetAnnotation.Comment.Value}";

            // Add the reply as a new comment
            if (associatedShape != null)
            {
                // Shape‑level reply (preserves the shape association)
                page.AddComment(associatedShape, replyText);
            }
            else
            {
                // Page‑level reply (no specific shape)
                // Use arbitrary coordinates (e.g., 1,1) for the comment position
                page.AddComment(1.0, 1.0, replyText);
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Reply added and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
