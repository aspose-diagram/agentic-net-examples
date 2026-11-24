using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

// Alias Aspose.Drawing types to avoid confusion with Aspose.Diagram types
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeGraphics = Aspose.Drawing.Graphics;
using AsposeColor = Aspose.Drawing.Color;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
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

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify picture (foreign) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Extract the original image bytes
                            byte[] originalBytes = shape.ForeignData.Value;
                            if (originalBytes == null || originalBytes.Length == 0)
                                continue; // Skip if no image data

                            // Load the image into Aspose.Drawing.Image
                            using (MemoryStream inputMs = new MemoryStream(originalBytes))
                            using (AsposeImage srcImage = AsposeImage.FromStream(inputMs))
                            using (AsposeBitmap srcBitmap = new AsposeBitmap(srcImage))
                            {
                                // Prepare a new bitmap to hold the grayscale result
                                using (AsposeBitmap grayBitmap = new AsposeBitmap(srcBitmap.Width, srcBitmap.Height))
                                using (AsposeGraphics graphics = AsposeGraphics.FromImage(grayBitmap))
                                {
                                    // Define a grayscale color matrix
                                    float[][] matrixElements = new float[][]
                                    {
                                        new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                        new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                                        new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                                        new float[] {0, 0, 0, 1, 0},
                                        new float[] {0, 0, 0, 0, 1}
                                    };
                                    ColorMatrix colorMatrix = new ColorMatrix(matrixElements);

                                    // Apply the color matrix via ImageAttributes
                                    using (ImageAttributes imgAttr = new ImageAttributes())
                                    {
                                        imgAttr.SetColorMatrix(
                                            colorMatrix,
                                            ColorMatrixFlag.Default,
                                            ColorAdjustType.Bitmap);

                                        // Draw the original image onto the new bitmap using the grayscale attributes
                                        graphics.DrawImage(
                                            srcBitmap,
                                            new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height),
                                            0,
                                            0,
                                            srcBitmap.Width,
                                            srcBitmap.Height,
                                            GraphicsUnit.Pixel,
                                            imgAttr);
                                    }

                                    // Save the grayscale bitmap back to a byte array (PNG format)
                                    using (MemoryStream outputMs = new MemoryStream())
                                    {
                                        grayBitmap.Save(outputMs, ImageFormat.Png);
                                        shape.ForeignData.Value = outputMs.ToArray();
                                    }
                                }
                            }
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Explicitly set the format tracker
                    SaveFormat = SaveFileFormat.Pdf,
                    // High‑resolution settings for embedded raster images
                    HorizontalResolution = 300,
                    VerticalResolution = 300,
                    JpegQuality = 100,
                    // Export hidden pages if any (optional, can be set to false)
                    ExportHiddenPage = false
                };

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Successfully saved PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}