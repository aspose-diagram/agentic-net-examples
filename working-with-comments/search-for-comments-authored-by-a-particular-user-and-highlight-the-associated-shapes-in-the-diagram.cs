using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, reviewer name to highlight
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: CommentHighlighter <input.vsdx> <output.vsdx> <ReviewerName>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string targetReviewer = args[2];

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Build a map of reviewer indices to reviewer names (Reviewer.ID does not exist)
            Dictionary<int, string> reviewerMap = new Dictionary<int, string>();
            int reviewerIndex = 0;
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                // Reviewer.Name is a Str2Value; use .Value to get the string
                reviewerMap[reviewerIndex] = reviewer.Name.Value;
                reviewerIndex++;
            }

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the annotations (comments) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Get the reviewer index for this comment
                    int reviewerId = annotation.ReviewerID.Value;

                    // Resolve the reviewer name from the map
                    if (reviewerMap.TryGetValue(reviewerId, out string reviewerName))
                    {
                        // Check if this comment is authored by the target reviewer
                        if (string.Equals(reviewerName, targetReviewer, StringComparison.OrdinalIgnoreCase))
                        {
                            // Get the shape ID associated with the comment (0 means no shape)
                            int shapeId = annotation.ShapeID;
                            if (shapeId != 0)
                            {
                                // Retrieve the shape from the page using its ID
                                Shape shape = page.Shapes.GetShape((long)shapeId);
                                if (shape != null)
                                {
                                    // Highlight the shape: set a red border and yellow fill
                                    shape.Line.LineColor.Value = "#FF0000";      // Red border
                                    shape.Line.LineWeight.Value = 0.03;          // Thicker line
                                    shape.Fill.FillForegnd.Value = "#FFFF00";    // Yellow fill
                                    Console.WriteLine($"Highlighted shape ID {shapeId} on page '{page.Name}'.");
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the output path using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream for visibility
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}