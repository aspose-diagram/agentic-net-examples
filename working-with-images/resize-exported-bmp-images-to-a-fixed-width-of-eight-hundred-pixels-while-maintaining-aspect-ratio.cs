using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string visioPath = "input.vsdx";
        // Verify the Visio file exists before proceeding
        if (!File.Exists(visioPath)) { Console.Error.WriteLine($"File not found: {visioPath}"); return; }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(visioPath);
            // Ensure the diagram is disposed at the end of processing
            using (diagram)
            {
                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Export the current page as BMP using Aspose.Diagram's ImageSaveOptions
                    string bmpPath = $"Page_{page.ID}_original.bmp";
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Bmp);
                    imgOptions.PageIndex = pageIndex; // zero‑based page index
                    diagram.Save(bmpPath, imgOptions);

                    // Resize the exported BMP to a width of 800 pixels while keeping aspect ratio
                    using (Aspose.Drawing.Image originalImage = Aspose.Drawing.Image.FromFile(bmpPath))
                    {
                        int originalWidth = originalImage.Width;
                        int originalHeight = originalImage.Height;

                        // Desired width
                        int targetWidth = 800;
                        // Calculate proportional height
                        int targetHeight = (int)(originalHeight * (targetWidth / (double)originalWidth));

                        // Create a new bitmap with the target dimensions
                        using (Aspose.Drawing.Bitmap resizedBitmap = new Aspose.Drawing.Bitmap(targetWidth, targetHeight))
                        {
                            // Draw the original image onto the new bitmap with high‑quality scaling
                            using (Aspose.Drawing.Graphics graphics = Aspose.Drawing.Graphics.FromImage(resizedBitmap))
                            {
                                graphics.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);
                            }

                            // Overwrite the original BMP with the resized version
                            resizedBitmap.Save(bmpPath, Aspose.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }

                    Console.WriteLine($"Page {page.ID} exported and resized to 800px width: {bmpPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}