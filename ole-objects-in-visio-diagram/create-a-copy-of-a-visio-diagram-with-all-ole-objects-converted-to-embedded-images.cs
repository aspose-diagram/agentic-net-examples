using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "source.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "copy_with_images.vsdx";

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect all OLE (foreign) shapes together with their containing pages
            List<(Page page, Shape shape)> oleShapes = new List<(Page, Shape)>();

            // Iterate through each page and shape to find OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is a foreign OLE object and has binary data
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Store both the page reference and the shape for later processing
                        oleShapes.Add((page, shape));
                    }
                }
            }

            // Process each OLE shape: render to image, remove OLE shape, insert image shape
            foreach (var entry in oleShapes)
            {
                Page page = entry.page;   // The page that contains the OLE shape
                Shape oleShape = entry.shape;

                // Capture shape geometry (position and size)
                double pinX = oleShape.XForm.PinX.Value;
                double pinY = oleShape.XForm.PinY.Value;
                double width = oleShape.XForm.Width.Value;
                double height = oleShape.XForm.Height.Value;

                // Render OLE content to a PNG image in memory
                using (MemoryStream imageStream = new MemoryStream())
                {
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Convert the OLE shape to an image and write it to the stream
                    oleShape.ToImage(imageStream, imgOptions);
                    imageStream.Position = 0; // Reset stream position for reading

                    // Remove the original OLE shape from the page
                    page.Shapes.Remove(oleShape);

                    // Insert the rendered image as a new shape on the same page
                    long newShapeId = page.AddShape(pinX, pinY, width, height, imageStream);
                    // Optional: retrieve the new shape if further processing is needed
                    // Shape newShape = page.Shapes.GetShape(newShapeId);
                }
            }

            // Save the modified diagram as a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram copied and OLE objects converted to images successfully.");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}