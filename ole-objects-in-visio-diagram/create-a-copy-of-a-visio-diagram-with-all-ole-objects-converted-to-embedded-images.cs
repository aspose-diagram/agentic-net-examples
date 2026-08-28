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
                // Output Visio file path (copy with OLE objects replaced by images)
                string outputPath = "output_converted.vsdx";

                // Load the source diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of OLE shapes to process (cannot modify collection while iterating)
                    var oleShapeIds = new System.Collections.Generic.List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify OLE objects: Type must be Foreign and ForeignType must be Object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            oleShapeIds.Add(shape.ID);
                        }
                    }

                    // Process each OLE shape
                    foreach (long shapeId in oleShapeIds)
                    {
                        // Retrieve the shape instance
                        Shape oleShape = page.Shapes.GetShape(shapeId);

                        // Export the OLE shape to an image (PNG) using a memory stream
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            oleShape.ToImage(imageStream, imgOptions);
                            imageStream.Position = 0; // Reset stream for reading

                            // Preserve original geometry
                            double pinX = oleShape.XForm.PinX.Value;
                            double pinY = oleShape.XForm.PinY.Value;
                            double width = oleShape.XForm.Width.Value;
                            double height = oleShape.XForm.Height.Value;

                            // Remove the original OLE shape from the page
                            page.Shapes.Remove(oleShape);

                            // Insert a new picture shape using the exported image stream
                            long newShapeId = page.AddShape(pinX, pinY, width, height, imageStream);
                            // (Optional) Retrieve the newly added shape if further adjustments are needed
                            // Shape imageShape = page.Shapes.GetShape(newShapeId);
                        }
                    }
                }

                // Save the modified diagram to a new file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }