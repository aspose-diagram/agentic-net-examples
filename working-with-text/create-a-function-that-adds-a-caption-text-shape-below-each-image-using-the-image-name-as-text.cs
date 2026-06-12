using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file with captions
                string outputPath = "output_with_captions.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Add captions below each image shape
                AddCaptionsBelowImages(diagram);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Iterates through all pages and shapes, finds image (foreign) shapes,
        /// and adds a text shape directly below each image using the shape's name as caption.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
        private static void AddCaptionsBelowImages(Diagram diagram)
        {
            // Offset in inches between the image bottom and the caption top
            const double verticalOffset = 0.2;

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs first to avoid modifying the collection while iterating
                var shapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }

                // Process each shape by its ID
                foreach (long shapeId in shapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify image shapes by TypeValue.Foreign
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Determine caption text: use shape.Name if available, otherwise fallback to Data1
                        string captionText = !string.IsNullOrWhiteSpace(shape.Name) ? shape.Name : shape.Data1;
                        if (string.IsNullOrWhiteSpace(captionText))
                            continue; // No meaningful name to use as caption

                        // Retrieve image geometry
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate position for the caption (centered horizontally, below the image)
                        double captionPinX = pinX;
                        double captionPinY = pinY - (height / 2) - verticalOffset - (0.1); // additional half of caption height

                        // Define caption size (width matches image width, height small)
                        double captionWidth = width;
                        double captionHeight = 0.2; // 0.2 inches height for the text box

                        // Add the text shape to the page
                        Shape captionShape = page.AddText(
                            captionPinX,
                            captionPinY,
                            captionWidth,
                            captionHeight,
                            captionText,
                            "Calibri",          // Font name
                            "#000000",          // Font color (black)
                            0.12);              // Font size in inches (≈8.64 points)

                        // Optional: ensure the caption is sent to back so it doesn't cover other content
                        captionShape.SendToBack();
                    }
                }
            }
        }
    }