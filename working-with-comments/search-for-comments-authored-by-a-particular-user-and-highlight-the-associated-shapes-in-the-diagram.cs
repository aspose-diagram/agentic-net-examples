using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, reviewer ID to search for
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <reviewerId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse reviewer ID
        if (!int.TryParse(args[2], out int targetReviewerId))
        {
            Console.Error.WriteLine("Invalid reviewer ID. It must be an integer.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the reviewer ID of the comment (if any)
                        int reviewerId = annotation.ReviewerID?.Value ?? 0;

                        // Skip comments not authored by the target reviewer
                        if (reviewerId != targetReviewerId)
                            continue;

                        // Get the shape ID associated with the comment (0 if none)
                        int shapeId = annotation.ShapeID;

                        // If the comment is not linked to a shape, skip it
                        if (shapeId <= 0)
                            continue;

                        // Retrieve the shape by its ID
                        Shape shape = page.Shapes.GetShape((long)shapeId);

                        // Ensure the shape exists and is not deleted
                        if (shape == null || shape.Del == BOOL.True)
                            continue;

                        // Highlight the shape by setting a bright yellow fill color
                        // FillForegnd is a ColorValue; assign using its .Value property with a hex string
                        shape.Fill.FillForegnd.Value = "#FFFF00";
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}