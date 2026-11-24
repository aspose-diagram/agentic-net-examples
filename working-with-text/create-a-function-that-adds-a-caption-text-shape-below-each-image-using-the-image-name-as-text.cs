using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (original name with suffix).
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_caption.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect all foreign (image) shapes together with their page.
            var imageShapes = new List<(Page page, Shape shape)>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes by Type == Foreign.
                    if (shape.Type == TypeValue.Foreign)
                    {
                        imageShapes.Add((page, shape));
                    }
                }
            }

            // Add a caption text shape below each image.
            foreach (var entry in imageShapes)
            {
                Page page = entry.page;
                Shape imgShape = entry.shape;

                // Retrieve geometry of the image shape.
                double pinX = imgShape.XForm.PinX.Value;
                double pinY = imgShape.XForm.PinY.Value;
                double width = imgShape.XForm.Width.Value;
                double height = imgShape.XForm.Height.Value;

                // Define caption dimensions.
                double captionHeight = 0.5; // reasonable height for a single line of text
                double captionWidth = width; // match image width

                // Compute position: directly below the image with a small gap.
                double gap = 0.1;
                double captionPinY = pinY - (height / 2) - (captionHeight / 2) - gap;
                double captionPinX = pinX;

                // Use the image shape's name as caption text. Fallback to empty string if null.
                string captionText = imgShape.Name ?? string.Empty;

                // Add the text shape to the page.
                page.AddText(captionPinX, captionPinY, captionWidth, captionHeight, captionText);
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with captions: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}