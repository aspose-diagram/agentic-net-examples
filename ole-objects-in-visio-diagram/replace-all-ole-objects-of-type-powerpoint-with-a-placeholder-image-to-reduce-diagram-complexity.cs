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
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        // Placeholder image to use for PowerPoint OLE objects
        string placeholderImagePath = "placeholder.png";
        // Guard: ensure placeholder image exists
        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"File not found: {placeholderImagePath}");
            return;
        }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Collect shapes that need replacement (PowerPoint OLE objects)
            var shapesToReplace = new List<(Page page, Shape shape)>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is an OLE foreign object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Identify PowerPoint OLE objects via the source file name
                        string sourceName = shape.ForeignData.ObjectSourceFullName;
                        if (!string.IsNullOrEmpty(sourceName) &&
                            sourceName.IndexOf(".ppt", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            shapesToReplace.Add((page, shape));
                        }
                    }
                }
            }

            // Replace each identified OLE shape with the placeholder image
            foreach (var entry in shapesToReplace)
            {
                Page page = entry.page;
                Shape oleShape = entry.shape;

                // Preserve position and size using XForm properties
                double pinX = oleShape.XForm.PinX.Value;
                double pinY = oleShape.XForm.PinY.Value; // corrected property
                double width = oleShape.XForm.Width.Value;
                double height = oleShape.XForm.Height.Value;

                // Remove the original OLE shape from the page
                page.Shapes.Remove(oleShape);

                // Add the placeholder image shape at the same location and size
                using (FileStream imgStream = new FileStream(placeholderImagePath, FileMode.Open, FileAccess.Read))
                {
                    long newShapeId = page.AddShape(pinX, pinY, width, height, imgStream);
                    // Optionally retrieve the new shape if further modifications are needed
                    // Shape placeholderShape = page.Shapes.GetShape(newShapeId);
                }
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}