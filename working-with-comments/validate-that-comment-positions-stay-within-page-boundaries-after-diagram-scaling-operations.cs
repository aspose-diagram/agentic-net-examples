using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CommentPositionValidator <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Iterate through all annotations (comments) on the page.
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // If the comment is attached to a shape, validate the shape's position.
                    // ShapeID is a primitive int; 0 means the comment is not linked to a shape.
                    if (annotation.ShapeID != 0)
                    {
                        // Retrieve the shape associated with the comment.
                        Shape shape = page.Shapes.GetShape(annotation.ShapeID);
                        if (shape == null)
                        {
                            throw new Exception($"Shape with ID {annotation.ShapeID} not found on page '{page.Name}'.");
                        }

                        // Get the shape's PinX and PinY (center coordinates) in inches.
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Validate that the shape's center lies within the page boundaries.
                        if (pinX < 0 || pinX > pageWidth || pinY < 0 || pinY > pageHeight)
                        {
                            throw new Exception(
                                $"Comment attached to shape ID {annotation.ShapeID} on page '{page.Name}' is out of bounds. " +
                                $"PinX={pinX}, PinY={pinY}, PageWidth={pageWidth}, PageHeight={pageHeight}.");
                        }
                    }
                    else
                    {
                        // For page-level comments, Aspose.Diagram does not expose explicit coordinates.
                        // We log the comment text for manual review.
                        Console.WriteLine($"Page-level comment on page '{page.Name}': {annotation.Comment.Value}");
                    }
                }

                // Example scaling operation: reduce the printed size to 75% of original.
                // This does not modify shape coordinates but affects print/export scaling.
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                page.PageSheet.PrintProps.ScaleY.Value = 0.75;
            }

            // Save the modified diagram to the output path using the Vsdx format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }