using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Create an output subfolder for extracted images.
        string outputFolder = Path.Combine(folderPath, "ExtractedImages");
        Directory.CreateDirectory(outputFolder);

        // Get all VDX files in the folder (non-recursive).
        string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx");

        foreach (string vdxPath in vdxFiles)
        {
            // Guard: ensure the file exists (redundant after GetFiles, but kept per rules).
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found: {vdxPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    string filePrefix = Path.GetFileNameWithoutExtension(vdxPath);
                    int imageCounter = 1;

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify embedded images by checking the shape type.
                            if (shape.Type == TypeValue.Foreign && shape.ForeignData?.Value != null)
                            {
                                byte[] imageData = shape.ForeignData.Value;

                                // Determine image file extension based on magic numbers.
                                string extension = GetImageExtension(imageData);
                                if (string.IsNullOrEmpty(extension))
                                {
                                    // Default to .bin if format cannot be determined.
                                    extension = ".bin";
                                }

                                string outputFileName = $"{filePrefix}_Image{imageCounter}{extension}";
                                string outputPath = Path.Combine(outputFolder, outputFileName);

                                // Write the image bytes to disk.
                                File.WriteAllBytes(outputPath, imageData);
                                Console.WriteLine($"Extracted image: {outputPath}");

                                imageCounter++;
                            }
                        }
                    }

                    if (imageCounter == 1)
                    {
                        Console.WriteLine($"No embedded images found in: {vdxPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Determines a common image file extension based on the byte signature.
    /// Returns the extension including the leading dot (e.g., ".png").
    /// </summary>
    private static string GetImageExtension(byte[] data)
    {
        if (data == null || data.Length < 4)
            return null;

        // PNG signature: 89 50 4E 47
        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            return ".png";

        // JPEG signature: FF D8 FF
        if (data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
            return ".jpg";

        // GIF signature: 47 49 46 38
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
            return ".gif";

        // BMP signature: 42 4D
        if (data[0] == 0x42 && data[1] == 0x4D)
            return ".bmp";

        // Default: unknown format.
        return null;
    }
}