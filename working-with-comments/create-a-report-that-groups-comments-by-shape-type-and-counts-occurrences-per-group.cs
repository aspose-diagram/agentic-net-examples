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
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to hold comment counts per shape type (case‑insensitive)
                Dictionary<string, int> commentCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PageSheet and annotations collection
                    if (page.PageSheet?.Annotations == null)
                        continue;

                    // Iterate through each annotation (comment) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // ShapeID is a primitive int; no null‑conditional needed
                        int shapeId = annotation.ShapeID;

                        // Retrieve the shape linked to this annotation
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape == null)
                            continue; // Shape not found

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine the shape type using the master name if available
                        string shapeType = shape.Master?.Name ?? "Unknown";

                        // Increment the count for this shape type
                        if (commentCounts.ContainsKey(shapeType))
                            commentCounts[shapeType]++;
                        else
                            commentCounts[shapeType] = 1;
                    }
                }

                // Output the report to the console
                Console.WriteLine("Comments grouped by shape type:");
                foreach (KeyValuePair<string, int> kvp in commentCounts)
                {
                    Console.WriteLine($"Shape Type: {kvp.Key}, Comment Count: {kvp.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}