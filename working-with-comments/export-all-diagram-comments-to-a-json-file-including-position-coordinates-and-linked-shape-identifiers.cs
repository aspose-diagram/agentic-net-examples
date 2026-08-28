using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramCommentExporter
{
    // Model representing a comment for JSON serialization
    public class CommentInfo
    {
        public int PageId { get; set; }
        public int ShapeId { get; set; }          // 0 if not linked to a shape
        public double X { get; set; }             // X coordinate of the comment
        public double Y { get; set; }             // Y coordinate of the comment
        public string Text { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output JSON file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCommentExporter <input.vsdx> <output.json>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            var comments = new List<CommentInfo>();

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Annotations (comments) are stored in the PageSheet
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    var comment = new CommentInfo
                    {
                        PageId = page.ID,
                        ShapeId = annotation.ShapeID,               // primitive int, no .Value
                        X = annotation.X.Value,                     // X coordinate
                        Y = annotation.Y.Value,                     // Y coordinate
                        Text = annotation.Comment.Value            // comment text
                    };

                    comments.Add(comment);
                }
            }

            // Serialize to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(comments, jsonOptions);

            // Write JSON to the specified file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Exported {comments.Count} comments to '{outputPath}'.");
        }
    }
}