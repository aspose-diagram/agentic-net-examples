using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input VST file path and output sprite sheet path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: SpriteSheetGenerator <input.vst> <output.png>");
                return;
            }

            string vstPath = args[0];
            string outputPath = args[1];

            // Load the VST diagram (stencil). The constructor auto‑detects the format.
            Diagram diagram = new Diagram(vstPath);

            // Collect rendered page images
            List<Bitmap> pageBitmaps = new List<Bitmap>();
            int maxWidth = 0;
            int totalHeight = 0;

            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image export options for PNG
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.PageIndex = i; // zero‑based page index

                using (MemoryStream ms = new MemoryStream())
                {
                    // Export the page to a memory stream
                    diagram.Save(ms, imgOptions);
                    ms.Position = 0;

                    // Load the image with Aspose.Drawing
                    Bitmap bmp = new Bitmap(ms);
                    pageBitmaps.Add(bmp);

                    // Track dimensions for the final sprite sheet
                    if (bmp.Width > maxWidth) maxWidth = bmp.Width;
                    totalHeight += bmp.Height;
                }
            }

            // Create the final sprite sheet bitmap
            using (Bitmap spriteSheet = new Bitmap(maxWidth, totalHeight))
            {
                using (Graphics g = Graphics.FromImage(spriteSheet))
                {
                    g.Clear(Color.White); // optional background

                    int currentY = 0;
                    foreach (Bitmap bmp in pageBitmaps)
                    {
                        // Draw each page image stacked vertically
                        g.DrawImage(bmp, 0, currentY, bmp.Width, bmp.Height);
                        currentY += bmp.Height;
                        bmp.Dispose(); // release individual page bitmap
                    }
                }

                // Save the combined sprite sheet as PNG
                spriteSheet.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Sprite sheet saved to: {outputPath}");
            }
        }
    }