using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Input diagram file path
        Console.WriteLine("Enter the path to the Visio diagram file:");
        string inputPath = Console.ReadLine();

        // Input reviewer (author) name to search for
        Console.WriteLine("Enter the reviewer name whose comments should be highlighted:");
        string targetReviewer = Console.ReadLine();

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all annotations (comments) on the page
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // Retrieve the reviewer ID from the annotation
                int reviewerId = annotation.ReviewerID.Value;

                // Get the reviewer name safely
                string reviewerName = GetReviewerName(diagram, reviewerId);

                // If the reviewer matches the target, highlight the associated shape
                if (!string.IsNullOrEmpty(reviewerName) && reviewerName.Equals(targetReviewer, StringComparison.OrdinalIgnoreCase))
                {
                    // Retrieve the shape referenced by the annotation
                    Shape shape = page.Shapes.GetShape(annotation.ShapeID);

                    // Ensure the shape exists and is not deleted
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        // Highlight by setting a bright fill color (yellow)
                        shape.Fill.FillForegnd.Value = "#FFFF00";
                    }
                }
            }
        }

        // Save the modified diagram
        Console.WriteLine("Enter the output path for the highlighted diagram:");
        string outputPath = Console.ReadLine();
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine("Processing complete. Diagram saved to: " + outputPath);
    }

    // Helper method to obtain reviewer name by ID
    private static string GetReviewerName(Diagram diagram, int reviewerId)
    {
        // Validate reviewer index
        if (reviewerId >= 0 && reviewerId < diagram.DocumentSheet.Reviewers.Count)
        {
            Reviewer reviewer = diagram.DocumentSheet.Reviewers[reviewerId];
            // Reviewer.Name is a Str2Value; use .Value to get the string
            return reviewer.Name.Value;
        }

        return null;
    }
}
