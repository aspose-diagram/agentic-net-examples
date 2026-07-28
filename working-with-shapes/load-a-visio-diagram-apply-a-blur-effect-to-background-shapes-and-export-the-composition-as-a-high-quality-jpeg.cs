using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to locate background pages
            foreach (Page page in diagram.Pages)
            {
                // Background pages are indicated by BOOL.True
                if (page.Background == BOOL.True)
                {
                    // Apply a blur effect to each shape that contains an image (foreign) component
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Image != null)
                        {
                            // Set blur amount (range 0.0 – 1.0); adjust as needed
                            shape.Image.Blur.Value = 0.25;
                        }
                    }
                }
            }

            // Configure JPEG export options for high‑quality output
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            jpegOptions.Resolution = 300f;          // 300 DPI for high quality
            jpegOptions.JpegQuality = 100;          // Maximum JPEG quality (0‑100)

            // Save the modified diagram as a JPEG image
            string outputPath = "output.jpg";
            diagram.Save(outputPath, jpegOptions);
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}