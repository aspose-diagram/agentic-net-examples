using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from file
            Diagram diagram = new Diagram(inputPath);

            // --------------------------------------------------------------------
            // 1. Apply scaling to each page (e.g., 50% of original size)
            // --------------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                // ScaleX and ScaleY are expressed as a factor (1.0 = 100%)
                page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                page.PageSheet.PrintProps.ScaleY.Value = 0.5;
            }

            // --------------------------------------------------------------------
            // 2. Validate that comment positions remain within page boundaries
            // --------------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Iterate all annotations (comments) on the page
                foreach (Annotation comment in page.PageSheet.Annotations)
                {
                    // If the comment is attached to a shape, validate the shape's bounds
                    if (comment.ShapeID != 0)
                    {
                        // Retrieve the associated shape by its ID
                        Shape shape = page.Shapes.GetShape(comment.ShapeID);

                        // Shape centre coordinates
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Half of the shape's width/height
                        double halfWidth = shape.XForm.Width.Value / 2.0;
                        double halfHeight = shape.XForm.Height.Value / 2.0;

                        // Compute the shape's bounding box
                        double left = pinX - halfWidth;
                        double right = pinX + halfWidth;
                        double bottom = pinY - halfHeight;
                        double top = pinY + halfHeight;

                        // Validate that the bounding box stays inside the page limits
                        bool outOfBounds = left < 0 || right > pageWidth || bottom < 0 || top > pageHeight;

                        if (outOfBounds)
                        {
                            Console.Error.WriteLine($"Comment (ID {comment.MarkerIndex.Value}) attached to shape ID {shape.ID} exceeds page bounds on page '{page.Name}'.");
                        }
                    }
                    else
                    {
                        // For page‑level comments, use the comment's X/Y coordinates directly
                        double commentX = comment.X.Value;
                        double commentY = comment.Y.Value;

                        bool outOfBounds = commentX < 0 || commentX > pageWidth || commentY < 0 || commentY > pageHeight;

                        if (outOfBounds)
                        {
                            Console.Error.WriteLine($"Page comment (ID {comment.MarkerIndex.Value}) is out of bounds on page '{page.Name}'.");
                        }
                    }
                }
            }

            // Save the modified diagram back to disk using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}