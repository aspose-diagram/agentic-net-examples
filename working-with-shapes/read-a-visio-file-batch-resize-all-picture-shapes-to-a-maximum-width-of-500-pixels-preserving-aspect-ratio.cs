using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_resized.vsdx";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify picture shapes: foreign type with embedded image data
                    if (shape.Type == TypeValue.Foreign && shape.Image != null)
                    {
                        // Retrieve the raw image bytes stored in the shape
                        byte[] imageBytes = shape.ForeignData.Value;
                        if (imageBytes == null || imageBytes.Length == 0)
                            continue; // Skip if no image data is present

                        // Load the image using Aspose.Drawing to obtain pixel dimensions
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            int pixelWidth = img.Width;
                            int pixelHeight = img.Height;

                            // Resize only when the image width exceeds the 500‑pixel limit
                            if (pixelWidth > 500)
                            {
                                // Compute scaling factor to bring width down to 500 pixels
                                double scale = 500.0 / pixelWidth;

                                // Current shape size in inches (Visio uses inches for dimensions)
                                double currentWidthInches = shape.XForm.Width.Value;
                                double currentHeightInches = shape.XForm.Height.Value;

                                // New dimensions preserving the original aspect ratio
                                double newWidthInches = currentWidthInches * scale;
                                double newHeightInches = currentHeightInches * scale;

                                // Apply the resized dimensions back to the shape
                                shape.XForm.Width.Value = newWidthInches;
                                shape.XForm.Height.Value = newHeightInches;

                                Console.WriteLine($"Resized shape ID {shape.ID} on page '{page.Name}' to width {newWidthInches:F2} inches.");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the output file using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}