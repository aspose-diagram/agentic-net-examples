using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Define output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is a foreign (image) shape
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Retrieve the raw image data stored in the shape
                        byte[] imageData = shape.ForeignData.Value;
                        if (imageData == null || imageData.Length < 2) continue;

                        // Detect JPEG signature (0xFF 0xD8)
                        if (imageData[0] == 0xFF && imageData[1] == 0xD8)
                        {
                            // Convert JPEG bytes to PNG using Aspose.Drawing
                            using (MemoryStream jpegStream = new MemoryStream(imageData))
                            // Fully qualify Aspose.Drawing.Image to avoid ambiguity with Aspose.Diagram.Image
                            using (Aspose.Drawing.Image jpegImage = Aspose.Drawing.Image.FromStream(jpegStream))
                            using (MemoryStream pngStream = new MemoryStream())
                            {
                                // Save the JPEG image as PNG into the memory stream
                                jpegImage.Save(pngStream, ImageFormat.Png);
                                // Replace the shape's image data with the new PNG bytes
                                shape.ForeignData.Value = pngStream.ToArray();
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}