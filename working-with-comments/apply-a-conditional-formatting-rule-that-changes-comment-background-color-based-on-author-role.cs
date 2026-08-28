using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the input and output Visio files
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define role‑to‑color mapping (hex color strings)
            var roleColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Manager",   "#FFCCCC" }, // Light red
                { "Developer", "#CCFFCC" }, // Light green
                { "Tester",    "#CCCCFF" }  // Light blue
            };

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all annotations (comments) on the current page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the reviewer ID associated with the comment
                    int reviewerId = annotation.ReviewerID.Value;

                    // Determine the reviewer role (using the reviewer name as a placeholder for role)
                    string role = "Default";
                    // Access the reviewer directly by index (Reviewer.ID does not exist)
                    if (reviewerId >= 0 && reviewerId < diagram.DocumentSheet.Reviewers.Count)
                    {
                        var reviewer = diagram.DocumentSheet.Reviewers[reviewerId];
                        // Reviewer.Name is a Str2Value; use .Value to get the string
                        role = reviewer.Name.Value;
                    }

                    // Choose a background color based on the role
                    if (!roleColors.TryGetValue(role, out string bgColor))
                    {
                        // Fallback color if the role is not mapped
                        bgColor = "#FFFFFF"; // White
                    }

                    // Retrieve the shape referenced by the annotation and set its fill foreground color
                    long shapeId = annotation.ShapeID; // ShapeID is a long primitive
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        shape.Fill.FillForegnd.Value = bgColor;
                    }
                }
            }

            // Save the modified diagram to the output path using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}