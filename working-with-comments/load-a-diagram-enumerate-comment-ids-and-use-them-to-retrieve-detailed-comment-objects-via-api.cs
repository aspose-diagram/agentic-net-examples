using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through all annotations (comments) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the unique comment identifier
                    long commentId = annotation.MarkerIndex.Value;
                    Console.WriteLine($"Comment ID: {commentId}");

                    // Retrieve detailed information about the comment
                    string commentText = annotation.Comment.Value;
                    int reviewerId = annotation.ReviewerID.Value;
                    int shapeId = annotation.ShapeID;
                    double posX = annotation.X.Value;
                    double posY = annotation.Y.Value;

                    Console.WriteLine($"Text       : {commentText}");
                    Console.WriteLine($"Reviewer ID: {reviewerId}");
                    Console.WriteLine($"Shape ID   : {shapeId}");
                    Console.WriteLine($"Position   : ({posX}, {posY})");
                    Console.WriteLine(new string('-', 40));
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
