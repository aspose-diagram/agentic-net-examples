using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (adjust index or name as needed)
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");
            Page page = diagram.Pages[0];

            // Retrieve a shape to which the original comment is attached
            if (page.Shapes.Count == 0)
                throw new Exception("The page contains no shapes.");
            Shape shape = page.Shapes[0];

            // Locate the existing comment (annotation) on the shape
            Annotation originalComment = null;
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                if (annotation.ShapeID == shape.ID)
                {
                    originalComment = annotation;
                    break;
                }
            }

            if (originalComment == null)
                throw new Exception("No existing comment found on the selected shape.");

            // Add a reply comment to the same shape
            string replyText = "This is a reply to the previous comment.";
            page.AddComment(shape, replyText);

            // Find the newly added comment (it will have the same shape ID and matching text)
            Annotation replyComment = null;
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                if (annotation.ShapeID == shape.ID && annotation.Comment.Value == replyText)
                {
                    replyComment = annotation;
                    break;
                }
            }

            // Preserve metadata: set the reviewer ID of the reply to match the original comment
            if (replyComment != null)
            {
                replyComment.ReviewerID.Value = originalComment.ReviewerID.Value;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
