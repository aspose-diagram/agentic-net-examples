using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeColor = Aspose.Drawing.Color;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath);

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape in the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify foreign (image) shapes
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            try
                            {
                                // Extract original image bytes
                                byte[] originalBytes = shape.ForeignData.Value;

                                // Load image using Aspose.Drawing
                                using (MemoryStream inputMs = new MemoryStream(originalBytes))
                                using (AsposeImage img = AsposeImage.FromStream(inputMs))
                                using (AsposeBitmap bmp = new AsposeBitmap(img))
                                {
                                    // Apply grayscale conversion pixel by pixel
                                    for (int y = 0; y < bmp.Height; y++)
                                    {
                                        for (int x = 0; x < bmp.Width; x++)
                                        {
                                            AsposeColor pixel = bmp.GetPixel(x, y);
                                            int gray = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                                            AsposeColor grayColor = AsposeColor.FromArgb(pixel.A, gray, gray, gray);
                                            bmp.SetPixel(x, y, grayColor);
                                        }
                                    }

                                    // Save modified image back to byte array (PNG format)
                                    using (MemoryStream outputMs = new MemoryStream())
                                    {
                                        bmp.Save(outputMs, ImageFormat.Png);
                                        shape.ForeignData.Value = outputMs.ToArray();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine($"Failed to process image in shape ID {shape.ID}: {ex.Message}");
                                // Continue processing other shapes
                            }
                        }
                    }
                }

                // Export each page as a separate TIFF file
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Configure image save options for TIFF
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                        saveOptions.PageIndex = pageIndex;   // zero‑based page index
                        saveOptions.PageCount = 1;           // export only the current page

                        // Build output file name
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.tiff");

                        // Save the page as TIFF
                        diagram.Save(outputPath, saveOptions);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to export page {pageIndex + 1} to TIFF: {ex.Message}");
                        // Continue with next page
                    }

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}