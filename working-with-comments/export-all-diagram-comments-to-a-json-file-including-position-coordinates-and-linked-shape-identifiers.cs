using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                var commentList = new List<object>();
                int pageIndex = 0;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Access page-level annotations (comments).
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Retrieve comment text.
                        string commentText = ann.Comment?.Value ?? string.Empty;

                        // Retrieve position coordinates if available.
                        double posX = ann.X?.Value ?? 0.0;
                        double posY = ann.Y?.Value ?? 0.0;

                        // Retrieve linked shape identifier (if any).
                        int shapeId = ann.ShapeID;

                        // Build a simple object representing the comment.
                        var commentInfo = new
                        {
                            PageIndex = pageIndex,
                            Text = commentText,
                            X = posX,
                            Y = posY,
                            ShapeID = shapeId
                        };

                        commentList.Add(commentInfo);
                    }

                    pageIndex++;
                }

                // Serialize the comment collection to JSON with indentation.
                string json = JsonSerializer.Serialize(commentList, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file.
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {commentList.Count} comments to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}