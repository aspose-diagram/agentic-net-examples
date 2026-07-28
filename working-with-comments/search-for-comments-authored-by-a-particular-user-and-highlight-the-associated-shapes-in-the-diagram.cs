using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output file with highlighted shapes
            string outputPath = "output_highlighted.vsdx";
            // Name of the reviewer whose comments should be highlighted
            string targetUser = "John Doe";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the reviewer ID that matches the target user name
            int targetReviewerId = -1;
            int reviewerIndex = 0;
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                if (reviewer.Name.Value == targetUser)
                {
                    targetReviewerId = reviewerIndex;
                    break;
                }
                reviewerIndex++;
            }

            if (targetReviewerId == -1)
            {
                Console.WriteLine($"Reviewer \"{targetUser}\" not found in the document.");
            }
            else
            {
                // Iterate through all pages and their annotations (comments)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Check if the comment was authored by the target reviewer
                        if (annotation.ReviewerID.Value == targetReviewerId)
                        {
                            // Retrieve the shape linked to this comment
                            long shapeId = annotation.ShapeID;
                            Shape shape = page.Shapes.GetShape(shapeId);
                            if (shape != null)
                            {
                                // Highlight the shape: red border and yellow fill
                                shape.Line.LineColor.Value = "#FF0000";
                                shape.Line.LineWeight.Value = 0.03;
                                shape.Fill.FillForegnd.Value = "#FFFF00";
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with highlighted shapes.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
