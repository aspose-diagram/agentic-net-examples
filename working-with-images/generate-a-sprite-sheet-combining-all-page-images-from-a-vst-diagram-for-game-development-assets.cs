using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VST file> <output sprite sheet PNG>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Lists to hold temporary image file paths and loaded images.
        List<string> tempImagePaths = new List<string>();
        List<Aspose.Drawing.Image> pageImages = new List<Aspose.Drawing.Image>();

        try
        {
            // Load the VST diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;

                // Export each page to a temporary PNG file and load it.
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure image save options for a single page.
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = i;   // zero‑based page index
                    imgOptions.PageCount = 1;   // export only this page

                    // Create a temporary file path for the page image.
                    string tempPath = Path.Combine(Path.GetTempPath(),
                        $"vst_page_{i}_{Guid.NewGuid()}.png");

                    // Save the page image.
                    diagram.Save(tempPath, imgOptions);
                    tempImagePaths.Add(tempPath);

                    // Load the exported image using Aspose.Drawing.
                    Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(tempPath);
                    pageImages.Add(img);
                }
            }

            // Determine sprite sheet dimensions (vertical stacking).
            int spriteWidth = 0;
            int spriteHeight = 0;
            foreach (Aspose.Drawing.Image img in pageImages)
            {
                if (img.Width > spriteWidth)
                    spriteWidth = img.Width;
                spriteHeight += img.Height;
            }

            // Create the final sprite sheet bitmap.
            using (Bitmap spriteSheet = new Bitmap(spriteWidth, spriteHeight))
            {
                using (Graphics g = Graphics.FromImage(spriteSheet))
                {
                    // Clear with transparent background.
                    g.Clear(Color.Transparent);

                    int offsetY = 0;
                    foreach (Aspose.Drawing.Image img in pageImages)
                    {
                        // Draw each page image at the current vertical offset.
                        g.DrawImage(img, new Rectangle(0, offsetY, img.Width, img.Height));
                        offsetY += img.Height;
                    }
                }

                // Save the combined sprite sheet to the requested output path.
                spriteSheet.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Sprite sheet created successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Clean up loaded images.
            foreach (Aspose.Drawing.Image img in pageImages)
            {
                try
                {
                    img.Dispose();
                }
                catch
                {
                    // Suppress any errors during image disposal.
                }
            }

            // Delete temporary image files.
            foreach (string tempPath in tempImagePaths)
            {
                try
                {
                    if (File.Exists(tempPath))
                        File.Delete(tempPath);
                }
                catch
                {
                    // Suppress any errors during cleanup.
                }
            }
        }
    }
}
