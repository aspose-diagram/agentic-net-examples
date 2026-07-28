using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Build a lookup of reviewer index to reviewer name
            Dictionary<int, string> reviewerNames = new Dictionary<int, string>();
            int reviewerIndex = 0;
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                // Reviewer.Name is a Str2Value; use .Value to get the string
                reviewerNames[reviewerIndex] = reviewer.Name.Value;
                reviewerIndex++;
            }

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all annotations (comments) on the current page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Get the reviewer index for this comment
                    int reviewerId = annotation.ReviewerID.Value;

                    // Determine the reviewer name; if not found, use empty string
                    string reviewerName = reviewerNames.ContainsKey(reviewerId) ? reviewerNames[reviewerId] : string.Empty;

                    // Decide background color based on role (author name)
                    string backgroundColor = GetColorForRole(reviewerName);

                    // Retrieve the shape associated with the comment (if any)
                    // ShapeID is an integer; use GetShape to obtain the shape instance
                    Shape shape = page.Shapes.GetShape(annotation.ShapeID);
                    if (shape != null)
                    {
                        // Apply solid fill pattern (1 = solid)
                        shape.Fill.FillPattern.Value = 1;
                        // Set the fill foreground color to the role‑based color
                        shape.Fill.FillForegnd.Value = backgroundColor;
                    }
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to map reviewer role/name to a hex color string
    private static string GetColorForRole(string reviewerName)
    {
        // Example role mapping; adjust as needed
        switch (reviewerName.Trim().ToLower())
        {
            case "manager":
                return "#FFCCCC"; // Light red
            case "developer":
                return "#CCFFCC"; // Light green
            case "tester":
                return "#CCCCFF"; // Light blue
            default:
                return "#FFFFFF"; // White (default)
        }
    }
}