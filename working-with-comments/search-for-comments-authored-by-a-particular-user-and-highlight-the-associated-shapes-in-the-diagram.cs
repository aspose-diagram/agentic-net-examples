using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";
                // Reviewer ID to filter comments (example value)
                int targetReviewerId = 1;

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Check if the comment was authored by the target reviewer
                        if (annotation.ReviewerID.Value == targetReviewerId)
                        {
                            // Retrieve the shape ID associated with the comment
                            int shapeId = annotation.ShapeID;

                            // Get the shape instance from the page's shape collection
                            Shape shape = page.Shapes.GetShape(shapeId);
                            if (shape != null)
                            {
                                // Highlight the shape by setting a yellow fill color
                                shape.Fill.FillForegnd.Value = "#FFFF00";
                                // Optionally, set a red outline for better visibility
                                shape.Line.LineColor.Value = "#FF0000";
                            }
                        }
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }