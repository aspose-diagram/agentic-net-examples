using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple DTO for JSON serialization
    private class CommentInfo
    {
        public string Comment { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int ShapeId { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var allComments = new List<CommentInfo>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access page-level annotations (comments)
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Extract comment text safely
                        string commentText = ann.Comment?.Value ?? string.Empty;

                        // Extract position coordinates (X, Y) if available
                        double x = ann.X?.Value ?? 0.0;
                        double y = ann.Y?.Value ?? 0.0;

                        // Extract linked shape identifier (0 if not linked)
                        int shapeId = ann.ShapeID;

                        // Add to the collection
                        allComments.Add(new CommentInfo
                        {
                            Comment = commentText,
                            X = x,
                            Y = y,
                            ShapeId = shapeId
                        });
                    }
                }

                // Serialize the comments list to JSON with indentation
                string json = JsonSerializer.Serialize(allComments, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Exported {allComments.Count} comments to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}