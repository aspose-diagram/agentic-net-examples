using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

// Alias Aspose.Drawing types to avoid conflict with Aspose.Diagram.Image
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeColor = Aspose.Drawing.Color;
using AsposeImageFormat = Aspose.Drawing.Imaging.ImageFormat;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only foreign (image) shapes that contain image data
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Load image bytes into Aspose.Drawing objects
                        using (MemoryStream inputStream = new MemoryStream(imageBytes))
                        using (AsposeImage originalImage = AsposeImage.FromStream(inputStream))
                        using (AsposeBitmap bitmap = new AsposeBitmap(originalImage))
                        {
                            // Apply sepia tone to each pixel
                            for (int y = 0; y < bitmap.Height; y++)
                            {
                                for (int x = 0; x < bitmap.Width; x++)
                                {
                                    AsposeColor originalColor = bitmap.GetPixel(x, y);

                                    double r = originalColor.R;
                                    double g = originalColor.G;
                                    double b = originalColor.B;

                                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                                    // Clamp values to 0‑255 range
                                    tr = tr > 255 ? 255 : tr;
                                    tg = tg > 255 ? 255 : tg;
                                    tb = tb > 255 ? 255 : tb;

                                    AsposeColor sepiaColor = AsposeColor.FromArgb(tr, tg, tb);
                                    bitmap.SetPixel(x, y, sepiaColor);
                                }
                            }

                            // Save the modified image back to a byte array (PNG format)
                            using (MemoryStream outputStream = new MemoryStream())
                            {
                                bitmap.Save(outputStream, AsposeImageFormat.Png);
                                shape.ForeignData.Value = outputStream.ToArray();
                            }
                        }
                    }
                }
            }

            // Export the diagram with the sepia‑toned images to PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
            };
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}