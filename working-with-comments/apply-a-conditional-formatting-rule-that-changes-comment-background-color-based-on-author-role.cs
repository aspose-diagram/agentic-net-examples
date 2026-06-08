using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Mapping of reviewer IDs to their roles
                var reviewerRoles = new Dictionary<int, string>
                {
                    { 1, "Manager" },
                    { 2, "Developer" },
                    { 3, "Tester" }
                    // Add more mappings as needed
                };

                // Mapping of roles to background color (hex string)
                var roleColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Manager", "#FFCCCC" },   // Light red
                    { "Developer", "#CCFFCC" }, // Light green
                    { "Tester", "#CCCCFF" }     // Light blue
                    // Add more role-color pairs as needed
                };

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has annotations collection
                    if (page.PageSheet.Annotations == null)
                        continue;

                    // Iterate through each comment (annotation) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Get the reviewer ID for the comment
                        int reviewerId = annotation.ReviewerID.Value;

                        // Determine the role; if not found, skip formatting
                        if (!reviewerRoles.TryGetValue(reviewerId, out string role))
                            continue;

                        // Determine the background color for the role; if not defined, skip
                        if (!roleColors.TryGetValue(role, out string colorHex))
                            continue;

                        // Retrieve the shape that represents the comment
                        // ShapeID is an int that references a shape on the same page
                        Shape commentShape = page.Shapes.GetShape(annotation.ShapeID);
                        if (commentShape == null)
                            continue;

                        // Apply solid fill pattern
                        commentShape.Fill.FillPattern.Value = 1; // 1 = solid fill

                        // Set the fill foreground color to the desired hex value
                        commentShape.Fill.FillForegnd.Value = colorHex;
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }