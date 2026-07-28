using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramCommentExporter
{
    // DTO for JSON serialization
    public class CommentInfo
    {
        public string PageName { get; set; } = string.Empty;
        public int ShapeId { get; set; }
        public string Text { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output JSON file path
                string outputPath = "comments.json";

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(inputPath);

                    var comments = new List<CommentInfo>();

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Access annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve comment text
                            string text = annotation.Comment.Value ?? string.Empty;

                            // Retrieve linked shape identifier (0 if not linked to a shape)
                            int shapeId = annotation.ShapeID;

                            // Retrieve position coordinates (if available)
                            // Annotation provides X and Y as DoubleValue; use .Value
                            double x = annotation.X?.Value ?? 0.0;
                            double y = annotation.Y?.Value ?? 0.0;

                            comments.Add(new CommentInfo
                            {
                                PageName = page.Name ?? string.Empty,
                                ShapeId = shapeId,
                                Text = text,
                                X = x,
                                Y = y
                            });
                        }
                    }

                    // Serialize to JSON with indentation
                    var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(comments, jsonOptions);

                    // Write JSON to file
                    File.WriteAllText(outputPath, json);

                    Console.WriteLine($"Exported {comments.Count} comments to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}