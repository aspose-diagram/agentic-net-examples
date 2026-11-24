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

        // Guard: ensure the input file exists
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
                // Sample mapping of reviewer IDs to roles
                var reviewerRoles = new Dictionary<int, string>
                {
                    { 1, "Manager" },
                    { 2, "Developer" },
                    { 3, "Tester" }
                };

                // Define background colors for each role (hex color strings)
                var roleColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Manager", "#FFCCCC" },   // Light red
                    { "Developer", "#CCFFCC" }, // Light green
                    { "Tester", "#CCCCFF" },    // Light blue
                    { "Other", "#FFFFFF" }      // White (default)
                };

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the reviewer (author) ID for the comment
                        int reviewerId = annotation.ReviewerID.Value;

                        // Determine the role based on the reviewer ID
                        string role;
                        if (!reviewerRoles.TryGetValue(reviewerId, out role))
                        {
                            role = "Other";
                        }

                        // Determine the background color for the role
                        string colorHex;
                        if (!roleColors.TryGetValue(role, out colorHex))
                        {
                            colorHex = "#FFFFFF"; // Fallback to white
                        }

                        // Find the shape that represents the comment bubble (if any)
                        Shape commentShape = null;
                        int targetShapeId = annotation.ShapeID; // ShapeID is an int, not nullable

                        if (targetShapeId != 0)
                        {
                            // Retrieve the shape by its ID from the current page
                            // Shapes.GetShape expects a long ID
                            commentShape = page.Shapes.GetShape((long)targetShapeId);
                        }

                        // If the shape is found, apply the background fill color
                        if (commentShape != null)
                        {
                            // Ensure the fill pattern is solid
                            commentShape.Fill.FillPattern.Value = 1; // Solid fill

                            // Set the foreground fill color to the role‑specific color
                            commentShape.Fill.FillForegnd.Value = colorHex;
                        }
                        else
                        {
                            // Log a warning if no associated shape is found
                            Console.Error.WriteLine($"Warning: No shape found for comment ID {annotation.MarkerIndex.Value}");
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}