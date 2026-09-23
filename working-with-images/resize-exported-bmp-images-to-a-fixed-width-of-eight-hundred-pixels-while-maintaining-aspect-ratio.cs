using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using AsposeDrawing = Aspose.Drawing;
using AsposeDrawingImaging = Aspose.Drawing.Imaging;
using AsposeDrawingDrawing2D = Aspose.Drawing.Drawing2D;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(diagramPath)) { Console.Error.WriteLine($"File not found: {diagramPath}"); return; }

        // Output folder for resized BMP images
        string outputFolder = "ResizedBmp";
        Directory.CreateDirectory(outputFolder);

        // Load the diagram inside a using block for proper disposal
        using (Diagram diagram = new Diagram(diagramPath))
        {
            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Export the page as BMP using ImageSaveOptions
                string tempBmpPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}_original.bmp");
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Bmp);
                imgOptions.PageIndex = pageIndex; // zero‑based page index

                try
                {
                    diagram.Save(tempBmpPath, imgOptions);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error exporting page {pageIndex + 1}: {ex.Message}");
                    continue;
                }

                // Load the exported BMP with Aspose.Drawing and resize it
                try
                {
                    using (AsposeDrawing.Image srcImage = AsposeDrawing.Image.FromFile(tempBmpPath))
                    {
                        const int targetWidth = 800; // desired width in pixels
                        double scaleFactor = (double)targetWidth / srcImage.Width;
                        int targetHeight = (int)(srcImage.Height * scaleFactor);

                        // Create a new bitmap with the target dimensions
                        using (AsposeDrawing.Bitmap resizedBitmap = new AsposeDrawing.Bitmap(targetWidth, targetHeight))
                        {
                            // Draw the source image onto the new bitmap with high‑quality scaling
                            using (AsposeDrawing.Graphics graphics = AsposeDrawing.Graphics.FromImage(resizedBitmap))
                            {
                                graphics.InterpolationMode = AsposeDrawingDrawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(srcImage, 0, 0, targetWidth, targetHeight);
                            }

                            // Save the resized BMP, overwriting the temporary file
                            string finalBmpPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}_800.bmp");
                            resizedBitmap.Save(finalBmpPath, AsposeDrawingImaging.ImageFormat.Bmp);
                        }
                    }

                    // Optionally delete the original un‑resized BMP
                    try { File.Delete(tempBmpPath); } catch { /* ignore deletion errors */ }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing image for page {pageIndex + 1}: {ex.Message}");
                    continue;
                }

                Console.WriteLine($"Page {pageIndex + 1} resized and saved.");
            }
        }

        Console.WriteLine("All pages processed.");
    }
}