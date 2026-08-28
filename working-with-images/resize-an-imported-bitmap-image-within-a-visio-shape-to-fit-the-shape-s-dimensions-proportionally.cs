using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing; // For image dimension extraction

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (must exist)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (will be overwritten if exists)
        string outputPath = "output_resized.vsdx";

        try
        {
            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify imported bitmap images (foreign shapes)
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        // Load the bitmap image from the shape's foreign data
                        using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            // Image dimensions in pixels
                            double imgWidthPx = img.Width;
                            double imgHeightPx = img.Height;

                            // Shape dimensions in inches (Visio internal units)
                            double shapeWidthIn = shape.XForm.Width.Value;
                            double shapeHeightIn = shape.XForm.Height.Value;

                            // Convert shape size to a comparable aspect ratio
                            double shapeAspect = shapeWidthIn / shapeHeightIn;
                            double imgAspect = imgWidthPx / imgHeightPx;

                            // Determine new shape dimensions that preserve image aspect ratio
                            double newWidthIn = shapeWidthIn;
                            double newHeightIn = shapeHeightIn;

                            if (imgAspect > shapeAspect)
                            {
                                // Image is wider relative to shape; limit by width
                                newHeightIn = shapeWidthIn / imgAspect;
                            }
                            else
                            {
                                // Image is taller relative to shape; limit by height
                                newWidthIn = shapeHeightIn * imgAspect;
                            }

                            // Apply the calculated dimensions back to the shape
                            shape.XForm.Width.Value = newWidthIn;
                            shape.XForm.Height.Value = newHeightIn;

                            // Center the image within the original bounding box by adjusting PinX/PinY
                            // (optional: keep the shape centered on its original center)
                            double deltaX = (shapeWidthIn - newWidthIn) / 2.0;
                            double deltaY = (shapeHeightIn - newHeightIn) / 2.0;
                            shape.XForm.PinX.Value += deltaX;
                            shape.XForm.PinY.Value += deltaY;
                        }
                    }
                }
            }

            // Save the modified diagram using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}