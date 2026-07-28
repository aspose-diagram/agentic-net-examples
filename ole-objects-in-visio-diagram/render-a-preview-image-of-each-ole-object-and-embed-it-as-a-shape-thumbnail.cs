using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (adjust the path as needed)
                string inputPath = "input.vsdx";

                // Output Visio file with OLE thumbnails embedded
                string outputPath = "output_with_thumbnails.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect OLE shapes first to avoid modifying the collection while iterating
                    List<Shape> oleShapes = new List<Shape>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            oleShapes.Add(shape);
                        }
                    }

                    // Process each OLE shape
                    foreach (Shape oleShape in oleShapes)
                    {
                        // Preserve original geometry
                        double pinX = oleShape.XForm.PinX.Value;
                        double pinY = oleShape.XForm.PinY.Value;
                        double width = oleShape.XForm.Width.Value;
                        double height = oleShape.XForm.Height.Value;

                        // Render the OLE object to a PNG image in memory
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            oleShape.ToImage(imageStream, imgOptions);
                            imageStream.Position = 0; // Reset stream for reading

                            // Add a new shape that uses the rendered image as its picture
                            long newShapeId = page.AddShape(pinX, pinY, width, height, imageStream);
                            Shape newShape = page.Shapes.GetShape(newShapeId);

                            // Optionally copy the original shape's name for reference
                            newShape.Name = oleShape.Name;
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