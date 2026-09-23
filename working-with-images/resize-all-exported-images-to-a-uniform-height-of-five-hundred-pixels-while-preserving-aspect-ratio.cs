using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

// Alias Aspose.Drawing image types to avoid conflict with Aspose.Diagram.Image
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeGraphics = Aspose.Drawing.Graphics;
using AsposeImageFormat = Aspose.Drawing.Imaging.ImageFormat;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";
        // Guard to ensure the source file exists
        if (!File.Exists(sourcePath)) { Console.Error.WriteLine($"File not found: {sourcePath}"); return; }

        // Directory to store the resized images
        string outputDir = "ResizedImages";
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the diagram within a using block for proper disposal
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Export the current page to a temporary PNG image
                    string tempPngPath = Path.Combine(outputDir, $"page_{page.ID}_temp.png");
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = page.ID, // Export only the current page
                        PageCount = 1
                    };
                    diagram.Save(tempPngPath, imgOptions);

                    // Load the exported image using Aspose.Drawing
                    using (AsposeImage srcImage = AsposeImage.FromFile(tempPngPath))
                    {
                        // Desired uniform height in pixels
                        int targetHeight = 500;

                        // Calculate the new width to preserve aspect ratio
                        int targetWidth = (int)Math.Round(srcImage.Width * (targetHeight / (double)srcImage.Height));

                        // Create a new bitmap with the target dimensions
                        using (AsposeBitmap resizedBitmap = new AsposeBitmap(targetWidth, targetHeight))
                        {
                            // Draw the source image onto the new bitmap, scaling it
                            using (AsposeGraphics graphics = AsposeGraphics.FromImage(resizedBitmap))
                            {
                                graphics.DrawImage(srcImage, 0, 0, targetWidth, targetHeight);
                            }

                            // Save the resized image, overwriting the temporary file
                            string finalPath = Path.Combine(outputDir, $"page_{page.ID}.png");
                            resizedBitmap.Save(finalPath, AsposeImageFormat.Png);
                        }
                    }

                    // Delete the temporary PNG file
                    File.Delete(tempPngPath);
                }
            }

            Console.WriteLine("All pages have been exported and resized to a height of 500 pixels.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}