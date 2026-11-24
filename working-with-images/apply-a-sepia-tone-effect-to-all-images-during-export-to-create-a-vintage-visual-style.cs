using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

// Alias Aspose.Drawing types for clarity
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeColor = Aspose.Drawing.Color;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against invalid output directory
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
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
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Ensure the shape contains image data
                            if (shape.ForeignData?.Value == null || shape.ForeignData.Value.Length == 0)
                                continue;

                            // Load the embedded image into a bitmap
                            byte[] imageBytes = shape.ForeignData.Value;
                            using (MemoryStream msInput = new MemoryStream(imageBytes))
                            using (AsposeBitmap bitmap = new AsposeBitmap(msInput))
                            {
                                // Apply sepia tone per pixel
                                for (int y = 0; y < bitmap.Height; y++)
                                {
                                    for (int x = 0; x < bitmap.Width; x++)
                                    {
                                        AsposeColor original = bitmap.GetPixel(x, y);
                                        int r = original.R;
                                        int g = original.G;
                                        int b = original.B;

                                        int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                                        int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                                        int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                                        // Clamp each component to 255
                                        tr = tr > 255 ? 255 : tr;
                                        tg = tg > 255 ? 255 : tg;
                                        tb = tb > 255 ? 255 : tb;

                                        AsposeColor sepia = AsposeColor.FromArgb(tr, tg, tb);
                                        bitmap.SetPixel(x, y, sepia);
                                    }
                                }

                                // Save the modified bitmap back to a byte array (PNG format)
                                using (MemoryStream msOutput = new MemoryStream())
                                {
                                    bitmap.Save(msOutput, ImageFormat.Png);
                                    shape.ForeignData.Value = msOutput.ToArray();
                                }
                            }
                        }
                    }
                }

                // Configure image save options for PNG export
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export the entire diagram with the sepia‑toned images
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}