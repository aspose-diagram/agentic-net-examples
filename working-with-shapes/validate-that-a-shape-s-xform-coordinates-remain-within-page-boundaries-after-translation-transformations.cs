using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define translation offsets (in inches)
                double deltaX = 1.0; // move right by 1 inch
                double deltaY = 0.5; // move up by 0.5 inch

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply translation to the shape
                        shape.Move(deltaX, deltaY);

                        // Validate the shape's new position against page boundaries
                        ValidateShapeWithinPage(shape, pageWidth, pageHeight);
                    }
                }

                // Optionally save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Validation completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Checks whether the shape's bounding box stays within the page limits.
        /// Throws an exception if the shape exceeds the boundaries.
        /// </summary>
        /// <param name="shape">The shape to validate.</param>
        /// <param name="pageWidth">Width of the page in inches.</param>
        /// <param name="pageHeight">Height of the page in inches.</param>
        private static void ValidateShapeWithinPage(Shape shape, double pageWidth, double pageHeight)
        {
            // Retrieve shape geometry
            double pinX = shape.XForm.PinX.Value;
            double pinY = shape.XForm.PinY.Value;
            double width = shape.XForm.Width.Value;
            double height = shape.XForm.Height.Value;

            // Calculate bounding edges
            double left = pinX - (width / 2.0);
            double right = pinX + (width / 2.0);
            double bottom = pinY - (height / 2.0);
            double top = pinY + (height / 2.0);

            // Check horizontal boundaries
            if (left < 0.0 || right > pageWidth)
            {
                string message = $"Shape ID {shape.ID} exceeds horizontal page limits. Left={left}, Right={right}, PageWidth={pageWidth}";
                Console.WriteLine(message);
                throw new Exception(message);
            }

            // Check vertical boundaries
            if (bottom < 0.0 || top > pageHeight)
            {
                string message = $"Shape ID {shape.ID} exceeds vertical page limits. Bottom={bottom}, Top={top}, PageHeight={pageHeight}";
                Console.WriteLine(message);
                throw new Exception(message);
            }
        }
    }