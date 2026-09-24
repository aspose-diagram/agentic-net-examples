using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            // args[0] = input Visio file path
            // args[1] = output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCaptionExample <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            AddCaptionsBelowImages(inputPath, outputPath);
            Console.WriteLine($"Diagram saved with captions to: {outputPath}");
        }

        /// <summary>
        /// Loads a Visio diagram, finds all image (foreign) shapes, and adds a text shape
        /// directly below each image using the image's name as the caption.
        /// </summary>
        /// <param name="inputFile">Path to the source Visio file.</param>
        /// <param name="outputFile">Path where the modified Visio file will be saved.</param>
        static void AddCaptionsBelowImages(string inputFile, string outputFile)
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputFile);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes (foreign objects)
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Use the universal name of the shape as the caption text
                        string captionText = shape.NameU ?? "Image";

                        // Retrieve geometric data of the image shape
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Position the caption below the image
                        // Image bottom Y = PinY - (Height / 2)
                        // Add a small offset (0.2 inches) for spacing
                        double captionPinX = pinX;
                        double captionPinY = pinY - (height / 2) - 0.2;

                        // Define a reasonable size for the caption text shape
                        double captionWidth = width;
                        double captionHeight = 0.3; // height of the text box in inches

                        // Add the text shape to the page
                        // AddText returns a Shape object representing the new text shape
                        Shape captionShape = page.AddText(
                            captionPinX,
                            captionPinY,
                            captionWidth,
                            captionHeight,
                            captionText);

                        // Optional: center the text horizontally within the shape
                        // (Visio centers text by default, so no extra formatting needed)
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputFile, SaveFileFormat.Vsdx);
        }
    }