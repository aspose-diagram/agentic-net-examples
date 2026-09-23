using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

// Alias to avoid ambiguity between Aspose.Diagram.Image and Aspose.Drawing.Image
using DrawingImage = Aspose.Drawing.Image;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source VST diagram (Visio template)
        string vstPath = "input.vst";

        // Guard to ensure the input file exists
        if (!File.Exists(vstPath))
        {
            Console.Error.WriteLine($"File not found: {vstPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram using the appropriate LoadFileFormat
            diagram = new Diagram(vstPath, LoadFileFormat.Vst);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Collect rendered page images
        List<DrawingImage> pageImages = new List<DrawingImage>();
        int pageCount = diagram.Pages.Count;

        for (int i = 0; i < pageCount; i++)
        {
            // Configure image export options for a single page
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                PageIndex = i,          // zero‑based page index
                PageCount = 1,          // export only this page
                Resolution = 300,       // DPI (adjust as needed)
                ExportHiddenPage = false
            };

            // Export the page to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    diagram.Save(ms, imgOptions);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error exporting page {i}: {ex.Message}");
                    continue;
                }

                ms.Position = 0;
                // Load the image from the stream using the aliased DrawingImage type
                DrawingImage img = DrawingImage.FromStream(ms);
                pageImages.Add(img);
            }
        }

        // Determine sprite sheet dimensions (horizontal strip)
        int sheetWidth = 0;
        int sheetHeight = 0;
        foreach (DrawingImage img in pageImages)
        {
            sheetWidth += img.Width;
            if (img.Height > sheetHeight)
                sheetHeight = img.Height;
        }

        // Create the sprite sheet bitmap
        using (Bitmap spriteSheet = new Bitmap(sheetWidth, sheetHeight, PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(spriteSheet))
            {
                g.Clear(Color.Transparent);
                int offsetX = 0;
                foreach (DrawingImage img in pageImages)
                {
                    g.DrawImage(img, offsetX, 0, img.Width, img.Height);
                    offsetX += img.Width;
                    img.Dispose(); // release individual page image
                }
            }

            // Save the combined sprite sheet
            string outputPath = "sprite_sheet.png";
            try
            {
                spriteSheet.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Sprite sheet saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving sprite sheet: {ex.Message}");
            }
        }
    }
}