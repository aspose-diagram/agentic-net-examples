using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
                // Sample author‑to‑role mapping (must be pre‑populated)
                var authorRoles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Alice", "Manager" },
                    { "Bob", "Developer" }
                };

                // Role‑to‑background‑color mapping (hex color strings)
                var roleColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Manager", "#FFCCCC" },   // Light red
                    { "Developer", "#CCFFCC" }, // Light green
                    { "Default", "#FFFFFF" }    // White
                };

                // Build a list of reviewer names indexed by their position in the collection
                var reviewerNames = new List<string>();
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Reviewer.Name is Str2Value – use .Value to obtain the string
                    string name = reviewer.Name != null ? reviewer.Name.Value : string.Empty;
                    reviewerNames.Add(name);
                }

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the reviewer ID and resolve the reviewer name
                        int reviewerId = annotation.ReviewerID.Value;
                        string reviewerName = (reviewerId >= 0 && reviewerId < reviewerNames.Count)
                            ? reviewerNames[reviewerId]
                            : string.Empty;

                        // Determine the role for this reviewer (fallback to "Default")
                        if (!authorRoles.TryGetValue(reviewerName, out string role))
                        {
                            role = "Default";
                        }

                        // Determine the background color for the role (fallback to white)
                        if (!roleColors.TryGetValue(role, out string colorHex))
                        {
                            colorHex = "#FFFFFF";
                        }

                        // Locate the shape that represents the comment visual (if any)
                        long commentShapeId = annotation.ShapeID; // ShapeID is an int, cast to long
                        Shape commentShape = null;
                        try
                        {
                            commentShape = page.Shapes.GetShape(commentShapeId);
                        }
                        catch
                        {
                            // Shape not found – continue without applying formatting
                        }

                        // If the shape is found, apply the background fill
                        if (commentShape != null)
                        {
                            // Ensure solid fill pattern
                            commentShape.Fill.FillPattern.Value = 1; // 1 = solid
                            // Apply the color (hex string)
                            commentShape.Fill.FillForegnd.Value = colorHex;
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}