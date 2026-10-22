using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

// Alias Aspose.Drawing types to avoid ambiguity with Aspose.Diagram.Image
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeColor = Aspose.Drawing.Color;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only foreign (image) shapes.
                        if (shape.Type != TypeValue.Foreign)
                            continue;

                        // Ensure the shape contains embedded image data.
                        if (shape.ForeignData == null ||
                            shape.ForeignData.Value == null ||
                            shape.ForeignData.Value.Length == 0)
                            continue;

                        // Load the embedded image bytes into an Aspose.Drawing.Image.
                        byte[] originalBytes = shape.ForeignData.Value;
                        using (MemoryStream inputStream = new MemoryStream(originalBytes))
                        using (AsposeImage img = AsposeImage.FromStream(inputStream))
                        using (AsposeBitmap bitmap = new AsposeBitmap(img))
                        {
                            // Apply sepia tone per pixel.
                            for (int y = 0; y < bitmap.Height; y++)
                            {
                                for (int x = 0; x < bitmap.Width; x++)
                                {
                                    AsposeColor pixel = bitmap.GetPixel(x, y);
                                    int r = pixel.R;
                                    int g = pixel.G;
                                    int b = pixel.B;

                                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                                    // Clamp each component to the 0‑255 range.
                                    if (tr > 255) tr = 255;
                                    if (tg > 255) tg = 255;
                                    if (tb > 255) tb = 255;

                                    AsposeColor sepia = AsposeColor.FromArgb(pixel.A, tr, tg, tb);
                                    bitmap.SetPixel(x, y, sepia);
                                }
                            }

                            // Save the modified bitmap back to a byte array (PNG format).
                            using (MemoryStream outputStream = new MemoryStream())
                            {
                                bitmap.Save(outputStream, ImageFormat.Png);
                                shape.ForeignData.Value = outputStream.ToArray();
                            }
                        }
                    }
                }

                // Configure image export options (PNG format).
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export the whole diagram as a single image.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram exported with sepia‑toned images to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}