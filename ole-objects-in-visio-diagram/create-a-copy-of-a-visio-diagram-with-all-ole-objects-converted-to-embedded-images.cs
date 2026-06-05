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

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path (copy with OLE objects replaced by images)
                string outputPath = "output_converted.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Collect IDs of OLE shapes to process (avoid modifying collection while iterating)
                    List<long> oleShapeIds = new List<long>();

                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign (OLE) object and has valid ObjectData
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
                        Aspose.Diagram.Shape oleShape = page.Shapes.GetShape(shapeId);

                        // Capture geometry of the original shape
                        double pinX = oleShape.XForm.PinX.Value;
                        double pinY = oleShape.XForm.PinY.Value;
                        double width = oleShape.XForm.Width.Value;
                        double height = oleShape.XForm.Height.Value;

                        // Export the OLE shape to an image (PNG) in memory
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            oleShape.ToImage(imageStream, imgOptions);
                            imageStream.Position = 0; // Reset stream for reading

                            // Remove the original OLE shape from the page
                            page.Shapes.Remove(oleShape);

                            // Add a new shape using the exported image
                            long newShapeId = page.AddShape(pinX, pinY, width, height, imageStream);
                            // Optionally, you can retrieve the new shape if further adjustments are needed
                            // Aspose.Diagram.Shape imageShape = page.Shapes.GetShape(newShapeId);
                        }
                    }
                }

                // Save the modified diagram to a new file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram conversion completed. Output saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }