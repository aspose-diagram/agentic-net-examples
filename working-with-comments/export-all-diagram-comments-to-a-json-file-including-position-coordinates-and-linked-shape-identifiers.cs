using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramCommentExport
{
    // Model representing a comment with its details
    public class CommentInfo
    {
        public int Id { get; set; }               // Unique identifier of the comment (MarkerIndex)
        public string Text { get; set; }          // Comment text
        public int ReviewerId { get; set; }       // Index of the reviewer/author
        public int ShapeId { get; set; }          // Linked shape identifier (0 if none)
        public double? PinX { get; set; }         // X coordinate of the linked shape (null if no shape)
        public double? PinY { get; set; }         // Y coordinate of the linked shape (null if no shape)
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string diagramPath = "input.vsdx";

                // Output JSON file path
                string jsonOutputPath = "comments.json";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collection to hold all extracted comments
                List<CommentInfo> comments = new List<CommentInfo>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has annotations (comments)
                    if (page.PageSheet != null && page.PageSheet.Annotations != null)
                    {
                        // Iterate through each annotation on the page
                        foreach (Annotation ann in page.PageSheet.Annotations)
                        {
                            // Basic comment data
                            int commentId = ann.MarkerIndex.Value;
                            string commentText = ann.Comment.Value;
                            int reviewerId = ann.ReviewerID.Value;
                            int linkedShapeId = ann.ShapeID; // Primitive int, may be 0 if not linked

                            double? pinX = null;
                            double? pinY = null;

                            // If the comment is linked to a shape, retrieve its position
                            if (linkedShapeId != 0)
                            {
                                // Retrieve the shape by its ID
                                Shape linkedShape = page.Shapes.GetShape(linkedShapeId);
                                if (linkedShape != null)
                                {
                                    pinX = linkedShape.XForm.PinX.Value;
                                    pinY = linkedShape.XForm.PinY.Value;
                                }
                            }

                            // Add the comment information to the list
                            comments.Add(new CommentInfo
                            {
                                Id = commentId,
                                Text = commentText,
                                ReviewerId = reviewerId,
                                ShapeId = linkedShapeId,
                                PinX = pinX,
                                PinY = pinY
                            });
                        }
                    }
                }

                // Serialize the comment list to JSON with indentation
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(comments, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(jsonOutputPath, json);

                Console.WriteLine($"Exported {comments.Count} comments to '{jsonOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}