using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_resized.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify imported bitmap images (foreign shapes with image data)
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.Value != null &&
                        shape.ForeignData.Value.Length > 0)
                    {
                        // Load the bitmap from the foreign data stream
                        using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                        {
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                            {
                                // Assume 96 DPI for conversion from pixels to inches
                                double imgWidthInches = img.Width / 96.0;
                                double imgHeightInches = img.Height / 96.0;

                                // Current shape dimensions (in inches)
                                double shapeWidth = shape.XForm.Width.Value;
                                double shapeHeight = shape.XForm.Height.Value;

                                // Compute aspect ratios
                                double imgAspect = imgWidthInches / imgHeightInches;
                                double shapeAspect = shapeWidth / shapeHeight;

                                double newWidth, newHeight;

                                // Scale proportionally to fit within the shape bounds
                                if (imgAspect > shapeAspect)
                                {
                                    // Image is wider relative to shape – fit width
                                    newWidth = shapeWidth;
                                    newHeight = shapeWidth / imgAspect;
                                }
                                else
                                {
                                    // Image is taller – fit height
                                    newHeight = shapeHeight;
                                    newWidth = shapeHeight * imgAspect;
                                }

                                // Apply the new dimensions to the shape
                                shape.XForm.Width.Value = newWidth;
                                shape.XForm.Height.Value = newHeight;
                            }
                        }
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
