using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output Visio file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only foreign (image) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Retrieve the raw image bytes stored in the shape
                        byte[] imageData = shape.ForeignData.Value;
                        if (imageData == null || imageData.Length < 2)
                            continue; // Skip if no image data

                        // Detect JPEG signature (0xFF, 0xD8)
                        if (imageData[0] == 0xFF && imageData[1] == 0xD8)
                        {
                            // Load JPEG image using Aspose.Drawing.Image (fully qualified to avoid ambiguity)
                            using (MemoryStream jpegStream = new MemoryStream(imageData))
                            using (Aspose.Drawing.Image jpegImage = Aspose.Drawing.Image.FromStream(jpegStream))
                            {
                                // Convert the JPEG to PNG and write back to the shape
                                using (MemoryStream pngStream = new MemoryStream())
                                {
                                    jpegImage.Save(pngStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                                    shape.ForeignData.Value = pngStream.ToArray();
                                }
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
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}