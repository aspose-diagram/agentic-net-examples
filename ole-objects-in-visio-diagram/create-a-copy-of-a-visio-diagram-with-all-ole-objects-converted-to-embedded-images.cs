using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VisioOleToImageCopy <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Collect OLE shapes first because we will modify the collection later
                List<Shape> shapesToProcess = new List<Shape>();
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify OLE objects: must be a foreign shape with valid ObjectData
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        shapesToProcess.Add(shape);
                    }
                }

                // Process each identified OLE shape
                foreach (Aspose.Diagram.Shape oleShape in shapesToProcess)
                {
                    // Capture the original geometry of the OLE shape
                    double pinX = oleShape.XForm.PinX.Value;
                    double pinY = oleShape.XForm.PinY.Value;
                    double width = oleShape.XForm.Width.Value;
                    double height = oleShape.XForm.Height.Value;

                    // Render the OLE shape to an image (PNG) in memory
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        oleShape.ToImage(imageStream, imgOptions);
                        imageStream.Position = 0; // Reset stream for reading

                        // Remove the original OLE shape from the page
                        page.Shapes.Remove(oleShape);

                        // Add a new image shape using the rendered image stream
                        long newShapeId = page.AddShape(pinX, pinY, width, height, imageStream);
                        // Retrieve the newly added shape (optional, can be used for further adjustments)
                        Shape imageShape = page.Shapes.GetShape(newShapeId);
                        // Ensure the new shape is not deleted
                        if (imageShape.Del == BOOL.True)
                        {
                            imageShape.Del = BOOL.False;
                        }
                    }
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}