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
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VST file> <output sprite sheet PNG>");
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

        // Temporary folder for per‑page PNGs
        string tempFolder = Path.Combine(Path.GetTempPath(), "VstSpriteTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Load the VST diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;
                var pageBitmaps = new List<Bitmap>();
                int maxWidth = 0;
                int totalHeight = 0;

                // Export each page to a PNG file and load it into a Bitmap
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure image save options for a single page
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = i,
                        PageCount = 1,
                        Resolution = 96f // DPI
                    };

                    string pageImagePath = Path.Combine(tempFolder, $"page_{i}.png");

                    // Save the page as PNG
                    diagram.Save(pageImagePath, imgOptions);

                    // Load the saved PNG into a Bitmap
                    Bitmap bmp = new Bitmap(pageImagePath);
                    pageBitmaps.Add(bmp);

                    // Update sprite sheet dimensions
                    if (bmp.Width > maxWidth) maxWidth = bmp.Width;
                    totalHeight += bmp.Height;
                }

                // Create the final sprite sheet bitmap
                using (Bitmap spriteSheet = new Bitmap(maxWidth, totalHeight))
                {
                    using (Graphics g = Graphics.FromImage(spriteSheet))
                    {
                        // Clear with transparent background
                        g.Clear(Color.Transparent);
                        int currentY = 0;

                        // Draw each page bitmap onto the sprite sheet vertically
                        foreach (Bitmap pageBmp in pageBitmaps)
                        {
                            g.DrawImage(pageBmp, 0, currentY, pageBmp.Width, pageBmp.Height);
                            currentY += pageBmp.Height;
                            pageBmp.Dispose(); // Dispose individual page bitmap after drawing
                        }
                    }

                    // Save the combined sprite sheet as PNG
                    spriteSheet.Save(outputPath, ImageFormat.Png);
                }
            }

            Console.WriteLine($"Sprite sheet created successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
        finally
        {
            // Clean up temporary files
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}