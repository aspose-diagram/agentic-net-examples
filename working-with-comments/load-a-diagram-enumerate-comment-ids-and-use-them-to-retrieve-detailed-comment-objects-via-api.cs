using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the annotations (comments) collection on the page sheet
                foreach (Annotation comment in page.PageSheet.Annotations)
                {
                    // Retrieve the unique comment identifier
                    long commentId = comment.MarkerIndex.Value;

                    // Retrieve detailed comment information
                    string commentText = comment.Comment.Value;
                    int reviewerId = comment.ReviewerID.Value;

                    // Output comment details
                    Console.WriteLine($"Page: {page.Name} | Comment ID: {commentId} | Reviewer ID: {reviewerId} | Text: {commentText}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
