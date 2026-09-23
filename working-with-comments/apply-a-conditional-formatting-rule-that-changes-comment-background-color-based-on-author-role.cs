using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the current page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Determine the reviewer (author) of the comment
                        // ReviewerID is an IntValue; use its .Value property
                        int reviewerId = annotation.ReviewerID.Value;

                        // Choose a background color based on the reviewer role.
                        // For demonstration, reviewerId == 1 is considered "Manager",
                        // otherwise the comment is treated as "Contributor".
                        string fillColor;
                        if (reviewerId == 1)
                        {
                            // Manager – light green background
                            fillColor = "#CCFFCC";
                        }
                        else
                        {
                            // Contributor – light pink background
                            fillColor = "#FFCCCC";
                        }

                        // The comment is associated with a shape identified by ShapeID.
                        // Retrieve that shape to modify its fill properties.
                        int shapeId = annotation.ShapeID; // primitive int
                        Shape commentShape = page.Shapes.GetShape((long)shapeId);

                        // Apply a solid fill pattern
                        commentShape.Fill.FillPattern.Value = 1; // 1 = solid
                        // Set the chosen background color
                        commentShape.Fill.FillForegnd.Value = fillColor;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }