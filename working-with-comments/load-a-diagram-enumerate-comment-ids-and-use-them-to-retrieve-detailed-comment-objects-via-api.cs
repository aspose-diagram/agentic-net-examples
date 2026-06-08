using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                    // Access the collection of comments (annotations) on the page
                    foreach (Annotation comment in page.PageSheet.Annotations)
                    {
                        // Retrieve comment details
                        long commentId = comment.MarkerIndex.Value;
                        string commentText = comment.Comment.Value;
                        int reviewerId = comment.ReviewerID.Value;
                        int associatedShapeId = comment.ShapeID; // primitive int

                        Console.WriteLine($"  Comment ID: {commentId}");
                        Console.WriteLine($"    Text: {commentText}");
                        Console.WriteLine($"    Reviewer ID: {reviewerId}");
                        Console.WriteLine($"    Associated Shape ID: {associatedShapeId}");

                        // If the comment is linked to a shape, retrieve that shape
                        if (associatedShapeId != 0)
                        {
                            Shape shape = page.Shapes.GetShape(associatedShapeId);
                            if (shape != null)
                            {
                                Console.WriteLine($"    Shape Name: {shape.Name}");
                                Console.WriteLine($"    Shape Universal Name: {shape.NameU}");
                                Console.WriteLine($"    Shape Type: {shape.Type}");
                            }
                            else
                            {
                                Console.WriteLine("    Shape not found (invalid ID).");
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }