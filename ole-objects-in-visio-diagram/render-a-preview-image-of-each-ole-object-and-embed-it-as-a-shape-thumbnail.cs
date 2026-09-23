using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_with_thumbnails.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Collect OLE shape IDs to process (avoid modifying collection while iterating)
                    var oleShapeIds = new System.Collections.Generic.List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify shape is a foreign OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            oleShapeIds.Add(shape.ID);
                        }
                    }

                    // Process each OLE shape
                    foreach (long shapeId in oleShapeIds)
                    {
                        // Retrieve the shape instance
                        Shape oleShape = page.Shapes.GetShape(shapeId);

                        // Determine the size of the original shape
                        double shapeWidth = oleShape.XForm.Width.Value;
                        double shapeHeight = oleShape.XForm.Height.Value;
                        double pinX = oleShape.XForm.PinX.Value;
                        double pinY = oleShape.XForm.PinY.Value;

                        // Render the OLE object to a PNG image in memory
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            oleShape.ToImage(imageStream, imgOptions);
                            imageStream.Position = 0; // Reset stream position for reading

                            // Add a new image shape using the rendered PNG
                            // The AddShape method expects a stream containing the image data
                            long imageShapeId = page.AddShape(pinX, pinY, shapeWidth, shapeHeight, imageStream);
                            // Retrieve the newly added shape (optional, e.g., for further adjustments)
                            Shape imageShape = page.Shapes.GetShape(imageShapeId);
                            // Ensure the image shape is not locked for selection
                            imageShape.Protection.LockSelect.Value = BOOL.True;
                        }

                        // Remove the original OLE shape from the page
                        page.Shapes.Remove(oleShape);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }