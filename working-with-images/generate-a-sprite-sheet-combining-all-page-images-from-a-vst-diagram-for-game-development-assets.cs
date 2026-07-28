using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Path to the VST (stencil) file
        string vstPath = "assets.stencil.vst";
        // Guard: ensure the stencil file exists
        if (!File.Exists(vstPath)) { Console.Error.WriteLine($"File not found: {vstPath}"); return; }

        // Output sprite sheet file
        string outputPath = "sprite_sheet.png";

        try
        {
            // Load the diagram (stencil) containing pages
            Diagram diagram = new Diagram(vstPath);

            int pageCount = diagram.Pages.Count;
            if (pageCount == 0)
            {
                Console.WriteLine("No pages found in the diagram.");
                return;
            }

            // Store each page image in memory
            List<Aspose.Drawing.Image> pageImages = new List<Aspose.Drawing.Image>();
            int maxHeight = 0;
            int totalWidth = 0;

            for (int i = 0; i < pageCount; i++)
            {
                // Export the current page to PNG using ImageSaveOptions
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = i,
                    PageCount = 1
                };

                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the single page to a memory stream
                    diagram.Save(ms, imgOptions);
                    ms.Position = 0;
                    // Load the image from the stream using Aspose.Drawing.Image
                    Aspose.Drawing.Image pageImg = Aspose.Drawing.Image.FromStream(ms);
                    pageImages.Add(pageImg);

                    // Update sprite sheet dimensions
                    totalWidth += pageImg.Width;
                    if (pageImg.Height > maxHeight)
                        maxHeight = pageImg.Height;
                }
            }

            // Create the sprite sheet bitmap with the accumulated dimensions
            using (Aspose.Drawing.Bitmap spriteSheet = new Aspose.Drawing.Bitmap(totalWidth, maxHeight))
            {
                using (Aspose.Drawing.Graphics g = Aspose.Drawing.Graphics.FromImage(spriteSheet))
                {
                    // Fill background with white
                    g.Clear(Aspose.Drawing.Color.White);

                    int offsetX = 0;
                    // Draw each page image side by side
                    foreach (Aspose.Drawing.Image img in pageImages)
                    {
                        g.DrawImage(img, offsetX, 0, img.Width, img.Height);
                        offsetX += img.Width;
                    }
                }

                // Save the combined sprite sheet as PNG
                spriteSheet.Save(outputPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"Sprite sheet saved to: {outputPath}");
            }

            // Clean up individual page images
            foreach (Aspose.Drawing.Image img in pageImages)
            {
                img.Dispose();
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}