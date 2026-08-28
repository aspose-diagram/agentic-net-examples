using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output sprite sheet path (second argument)
        string inputPath = args.Length > 0 ? args[0] : "diagram.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "spritesheet.png";

        // List to hold each page image and its dimensions
        List<Aspose.Drawing.Image> pageImages = new List<Aspose.Drawing.Image>();
        int totalWidth = 0;
        int maxHeight = 0;

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PNG export options (one image per page)
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.PageCount = 1; // export a single page at a time

            int pageIndex = 0;
            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Set the page index for the current export
                pngOptions.PageIndex = pageIndex;

                // Create a temporary file for the exported PNG
                string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

                // Export the current page to the temporary PNG file
                diagram.Save(tempFile, pngOptions);

                // Load the exported PNG into an Aspose.Drawing.Image
                Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(tempFile);
                pageImages.Add(img);

                // Update sprite sheet dimensions
                totalWidth += img.Width;
                if (img.Height > maxHeight) maxHeight = img.Height;

                // Delete the temporary file
                File.Delete(tempFile);

                pageIndex++;
            }

            // Ensure at least one page was processed
            if (pageImages.Count == 0)
            {
                Console.Error.WriteLine("No pages were found in the diagram.");
                return;
            }

            // Create a new bitmap to hold the combined sprite sheet
            using (Aspose.Drawing.Bitmap spriteSheet = new Aspose.Drawing.Bitmap(totalWidth, maxHeight))
            {
                // Obtain a graphics object for drawing onto the bitmap
                using (Aspose.Drawing.Graphics g = Aspose.Drawing.Graphics.FromImage(spriteSheet))
                {
                    int offsetX = 0;
                    // Draw each page image side by side
                    foreach (Aspose.Drawing.Image img in pageImages)
                    {
                        g.DrawImage(img, offsetX, 0, img.Width, img.Height);
                        offsetX += img.Width;
                    }
                }

                // Save the final sprite sheet as PNG
                spriteSheet.Save(outputPath, ImageFormat.Png);
            }

            // Dispose all loaded page images
            foreach (Aspose.Drawing.Image img in pageImages)
            {
                img.Dispose();
            }

            Console.WriteLine($"Sprite sheet created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}