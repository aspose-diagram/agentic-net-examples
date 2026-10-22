using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, author name to search for.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <authorName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetAuthor = args[2];

        // Guard: ensure the input file exists.
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
                // Build a list of reviewer names indexed by ReviewerID.
                var reviewerNames = new System.Collections.Generic.List<string>();
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Reviewer.Name is a Str2Value; retrieve the string via .Value.
                    reviewerNames.Add(reviewer.Name?.Value ?? string.Empty);
                }

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Access page‑level annotations (comments).
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Resolve the author name using the ReviewerID.
                        int reviewerId = annotation.ReviewerID?.Value ?? -1;
                        string author = (reviewerId >= 0 && reviewerId < reviewerNames.Count)
                            ? reviewerNames[reviewerId]
                            : string.Empty;

                        // Compare with the target author (case‑insensitive).
                        if (!author.Equals(targetAuthor, StringComparison.OrdinalIgnoreCase))
                            continue; // Not the author we are looking for.

                        // Get the shape ID that the comment is attached to.
                        int shapeId = annotation.ShapeID; // Primitive int, no .Value.
                        if (shapeId == 0)
                            continue; // No shape associated.

                        // Retrieve the shape from the page's shape collection.
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape == null)
                            continue; // Shape not found.

                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Highlight the shape:
                        // - Set a bright fill color (yellow).
                        // - Set a thick red outline.
                        shape.Fill.FillForegnd.Value = "#FFFF00"; // Yellow
                        shape.Line.LineColor.Value = "#FF0000";   // Red
                        shape.Line.LineWeight.Value = 0.03;      // Thicker line (in inches)
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}