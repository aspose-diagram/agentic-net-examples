using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ExportComments
{
    // DTO for JSON serialization
    public class CommentInfo
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int ShapeId { get; set; }
        public double ShapePinX { get; set; }
        public double ShapePinY { get; set; }
    }

    public class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // List to hold extracted comments
                List<CommentInfo> comments = new List<CommentInfo>();

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access annotations (comments) on the current page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        CommentInfo info = new CommentInfo
                        {
                            Id = annotation.MarkerIndex.Value,
                            Text = annotation.Comment.Value,
                            ShapeId = annotation.ShapeID
                        };

                        // If the comment is linked to a shape, retrieve its coordinates
                        if (annotation.ShapeID != 0)
                        {
                            Shape linkedShape = page.Shapes.GetShape(annotation.ShapeID);
                            if (linkedShape != null)
                            {
                                info.ShapePinX = linkedShape.XForm.PinX.Value;
                                info.ShapePinY = linkedShape.XForm.PinY.Value;
                            }
                        }

                        comments.Add(info);
                    }
                }

                // Serialize the comment list to JSON with indentation
                string json = JsonSerializer.Serialize(comments, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON output to a file
                File.WriteAllText("comments.json", json);

                Console.WriteLine($"Exported {comments.Count} comments to comments.json");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}