using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Output Visio file path
        string outputPath = "output_resized.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only foreign (bitmap) shapes that contain image data
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        // Load the embedded bitmap using Aspose.Drawing.Image (fully qualified to avoid ambiguity)
                        using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            // Original image size in inches (consider DPI)
                            double imgWidthInches = img.Width / img.HorizontalResolution;
                            double imgHeightInches = img.Height / img.VerticalResolution;

                            // Current shape dimensions in inches
                            double shapeWidth = shape.XForm.Width.Value;
                            double shapeHeight = shape.XForm.Height.Value;

                            // Determine scaling factor to fit image proportionally within the shape
                            double scale = Math.Min(shapeWidth / imgWidthInches, shapeHeight / imgHeightInches);

                            // Calculate new dimensions while preserving aspect ratio
                            double newWidth = imgWidthInches * scale;
                            double newHeight = imgHeightInches * scale;

                            // Apply the new dimensions to the shape
                            shape.XForm.Width.Value = newWidth;
                            shape.XForm.Height.Value = newHeight;
                        }
                    }
                }
            }

            // Save the modified diagram to the output file using the appropriate SaveFileFormat enum
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}