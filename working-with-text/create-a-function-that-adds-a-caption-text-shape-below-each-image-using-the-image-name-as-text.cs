using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Add captions below each image shape
                AddImageCaptions(diagram);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Iterates through all pages and shapes, finds image (foreign) shapes,
        /// and adds a text shape directly below each image using the image's name as caption.
        /// </summary>
        /// <param name="diagram">The diagram to process.</param>
        static void AddImageCaptions(Diagram diagram)
        {
            // Offset in inches between the image bottom and the caption top
            const double verticalOffset = 0.2;

            // Font settings for the caption
            const string captionFont = "Arial";
            const string captionColor = "#000000"; // black
            const double captionFontSize = 0.2; // approx 14pt (points / 72)

            foreach (Page page in diagram.Pages)
            {
                // Collect shapes to avoid modifying the collection while iterating
                var shapes = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    shapes.Add(shape);
                }

                foreach (Shape shape in shapes)
                {
                    // Identify image shapes (foreign objects)
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Use the shape's Name as the caption text
                        string captionText = shape.Name ?? "Image";

                        // Retrieve image geometry
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate position for the caption (centered below the image)
                        double captionPinX = pinX;
                        double captionPinY = pinY - (height / 2) - verticalOffset;

                        // Width of the caption matches the image width; height is a small value
                        double captionWidth = width;
                        double captionHeight = verticalOffset;

                        // Add the caption text shape to the page
                        page.AddText(
                            captionPinX,
                            captionPinY,
                            captionWidth,
                            captionHeight,
                            captionText,
                            captionFont,
                            captionColor,
                            captionFontSize);
                    }
                }
            }
        }
    }