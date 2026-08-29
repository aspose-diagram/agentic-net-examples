using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output Visio file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure the directory for the output file exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Collect foreign (image) shapes first to avoid modifying the collection while iterating.
                var imageShapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes by TypeValue.Foreign.
                    if (shape.Type == TypeValue.Foreign)
                    {
                        imageShapeIds.Add(shape.ID);
                    }
                }

                // Process each image shape.
                foreach (long shapeId in imageShapeIds)
                {
                    Shape imgShape = page.Shapes.GetShape(shapeId);
                    if (imgShape == null) continue; // Safety check.

                    // Use the shape's universal name as the caption text (fallback to empty string).
                    string captionText = imgShape.NameU ?? string.Empty;

                    // Determine position for the caption: directly below the image.
                    double imgPinX = imgShape.XForm.PinX.Value;
                    double imgPinY = imgShape.XForm.PinY.Value;
                    double imgWidth = imgShape.XForm.Width.Value;
                    double imgHeight = imgShape.XForm.Height.Value;

                    // Bottom edge of the image.
                    double imgBottomY = imgPinY - (imgHeight / 2.0);

                    // Define caption dimensions.
                    double captionWidth = imgWidth;          // Same width as the image.
                    double captionHeight = 0.2;              // 0.2 inches height for the text box.
                    double margin = 0.05;                    // Small gap between image and caption.

                    // Center the caption horizontally with the image.
                    double captionPinX = imgPinX;

                    // Position the caption below the image, accounting for margin.
                    double captionPinY = imgBottomY - margin - (captionHeight / 2.0);

                    // Add a text shape (caption) to the page.
                    Shape captionShape = page.AddText(captionPinX, captionPinY, captionWidth, captionHeight, captionText);

                    // Optional: set a simple text formatting (e.g., center alignment).
                    // Align the text horizontally by setting the paragraph alignment.
                    if (captionShape.Paras.Count > 0)
                    {
                        captionShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with captions to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}