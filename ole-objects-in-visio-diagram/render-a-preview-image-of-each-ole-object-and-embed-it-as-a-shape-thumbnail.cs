using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs first to avoid modification during iteration
                List<long> shapeIds = new List<long>();
                foreach (Shape s in page.Shapes)
                {
                    shapeIds.Add(s.ID);
                }

                foreach (long shapeId in shapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Verify the shape is an OLE object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Generate a PNG thumbnail of the OLE shape into a memory stream
                        using (MemoryStream thumbnailStream = new MemoryStream())
                        {
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            shape.ToImage(thumbnailStream, imgOptions);
                            thumbnailStream.Position = 0; // Reset stream position for reading

                            // Determine placement for the thumbnail (offset to the right of the original shape)
                            double thumbPinX = shape.XForm.PinX.Value + shape.XForm.Width.Value + 0.5; // 0.5 inch offset
                            double thumbPinY = shape.XForm.PinY.Value;
                            double thumbWidth = shape.XForm.Width.Value;
                            double thumbHeight = shape.XForm.Height.Value;

                            // Add the thumbnail as a new picture shape on the same page
                            long thumbShapeId = page.AddShape(thumbPinX, thumbPinY, thumbWidth, thumbHeight, thumbnailStream);

                            // Optional: set a name for the thumbnail shape
                            Shape thumbShape = page.Shapes.GetShape(thumbShapeId);
                            thumbShape.Name = $"Thumbnail_{shape.ID}";
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}