using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Dictionary to hold comment counts per shape type
            Dictionary<string, int> commentCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page has annotations (comments)
                if (page.PageSheet?.Annotations == null)
                    continue;

                // Iterate over each annotation (comment) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the shape associated with the comment using ShapeID
                    Shape shape = null;
                    try
                    {
                        shape = page.Shapes.GetShape(annotation.ShapeID);
                    }
                    catch
                    {
                        // If the shape cannot be retrieved, skip this annotation
                        continue;
                    }

                    // Determine the shape type via its master name; fallback to "Unknown"
                    string shapeType = shape?.Master?.Name ?? "Unknown";

                    // Increment the count for this shape type
                    if (commentCounts.ContainsKey(shapeType))
                        commentCounts[shapeType]++;
                    else
                        commentCounts[shapeType] = 1;
                }
            }

            // Output the report
            Console.WriteLine("Comments grouped by shape type:");
            foreach (var kvp in commentCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}