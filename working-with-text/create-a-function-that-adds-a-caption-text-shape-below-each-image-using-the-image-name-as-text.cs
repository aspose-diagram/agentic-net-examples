using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional argument or auto‑generated)
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_caption.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect foreign (image) shapes first to avoid modifying collection during iteration
                var imageShapes = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign)
                    {
                        imageShapes.Add(shape);
                    }
                }

                // Add a caption below each image shape
                foreach (Shape imgShape in imageShapes)
                {
                    // Retrieve geometric data of the image shape
                    double pinX = imgShape.XForm.PinX.Value;
                    double pinY = imgShape.XForm.PinY.Value;
                    double width = imgShape.XForm.Width.Value;
                    double height = imgShape.XForm.Height.Value;

                    // Determine caption position: directly below the image with a small offset
                    const double offset = 0.2; // inches
                    double captionPinY = pinY - (height / 2) - offset;

                    // Use the shape's Name as the caption text (image file name or identifier)
                    string captionText = imgShape.Name ?? "Image";

                    // Add a text shape using Page.AddText (PinX, PinY, width, height, text)
                    // Height for the caption shape is set to a modest value (e.g., 0.3 inches)
                    page.AddText(pinX, captionPinY, width, 0.3, captionText);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with captions: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}