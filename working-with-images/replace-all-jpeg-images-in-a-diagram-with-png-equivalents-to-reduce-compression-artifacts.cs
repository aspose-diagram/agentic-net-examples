using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_png.vsdx");
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only foreign (image) shapes
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            byte[] imageData = shape.ForeignData.Value;

                            // Simple JPEG header check (0xFF,0xD8)
                            if (imageData.Length >= 2 && imageData[0] == 0xFF && imageData[1] == 0xD8)
                            {
                                // Convert JPEG to PNG using Aspose.Drawing
                                using (MemoryStream jpegStream = new MemoryStream(imageData))
                                {
                                    // Load the JPEG image
                                    using (Aspose.Drawing.Image jpegImage = Aspose.Drawing.Image.FromStream(jpegStream))
                                    {
                                        // Save as PNG into a new memory stream
                                        using (MemoryStream pngStream = new MemoryStream())
                                        {
                                            jpegImage.Save(pngStream, ImageFormat.Png);
                                            // Replace the shape's image data with PNG bytes
                                            shape.ForeignData.Value = pngStream.ToArray();
                                        }
                                    }
                                }

                                // Ensure the image shape stays behind other content
                                shape.SendToBack();
                            }
                        }
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}