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
                // Output Visio file path
                string outputPath = "output.vsdx";
                // Placeholder image file path (must exist)
                string placeholderImagePath = "placeholder.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Collect IDs of PowerPoint OLE shapes to replace
                    List<long> oleShapeIds = new List<long>();

                    // Enumerate shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE foreign object
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ObjectSourceFullName != null)
                        {
                            string sourceName = shape.ForeignData.ObjectSourceFullName;
                            // Check if the OLE object is a PowerPoint file
                            if (sourceName.EndsWith(".ppt", StringComparison.OrdinalIgnoreCase) ||
                                sourceName.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase))
                            {
                                oleShapeIds.Add(shape.ID);
                            }
                        }
                    }

                    // Replace each identified OLE shape with a placeholder image
                    foreach (long shapeId in oleShapeIds)
                    {
                        // Retrieve the original OLE shape
                        Aspose.Diagram.Shape oleShape = page.Shapes.GetShape(shapeId);

                        // Preserve position and size
                        double pinX = oleShape.XForm.PinX.Value;
                        double pinY = oleShape.XForm.PinY.Value;
                        double width = oleShape.XForm.Width.Value;
                        double height = oleShape.XForm.Height.Value;

                        // Remove the OLE shape from the page
                        page.Shapes.Remove(oleShape);

                        // Add a new shape containing the placeholder image
                        using (FileStream imgStream = new FileStream(placeholderImagePath, FileMode.Open, FileAccess.Read))
                        {
                            long newShapeId = page.AddShape(pinX, pinY, width, height, imgStream);
                            Aspose.Diagram.Shape placeholderShape = page.Shapes.GetShape(newShapeId);

                            // Optionally add a caption indicating replacement
                            placeholderShape.Text.Value.Clear();
                            placeholderShape.Text.Value.Add(new Txt("PowerPoint Placeholder"));
                        }
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