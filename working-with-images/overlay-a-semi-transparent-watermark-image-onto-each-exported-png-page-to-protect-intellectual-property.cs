using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <watermarkImagePath> <outputFolder>");
            return;
        }

        string inputVisioPath = args[0];
        string watermarkImagePath = args[1];
        string outputFolder = args[2];

        // Guard: input Visio file must exist.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        // Guard: watermark image file must exist.
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image file not found: {watermarkImagePath}");
            return;
        }

        // Ensure output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Load the watermark image once (will be reused for each page).
        using (Aspose.Drawing.Image watermarkImage = Aspose.Drawing.Image.FromFile(watermarkImagePath))
        {
            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputVisioPath);

                int pageCount = diagram.Pages.Count;

                // Iterate through each page and export with watermark.
                for (int i = 0; i < pageCount; i++)
                {
                    // Export the current page to a PNG image in memory.
                    using (MemoryStream pageStream = new MemoryStream())
                    {
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,
                            PageCount = 1
                        };

                        diagram.Save(pageStream, pngOptions);
                        pageStream.Position = 0;

                        // Load the exported PNG into Aspose.Drawing.
                        using (Aspose.Drawing.Image baseImage = Aspose.Drawing.Image.FromStream(pageStream))
                        {
                            // Prepare graphics object for drawing.
                            using (Aspose.Drawing.Graphics graphics = Aspose.Drawing.Graphics.FromImage(baseImage))
                            {
                                // Configure semi‑transparent rendering via ColorMatrix.
                                Aspose.Drawing.Imaging.ImageAttributes imgAttr = new Aspose.Drawing.Imaging.ImageAttributes();
                                Aspose.Drawing.Imaging.ColorMatrix colorMatrix = new Aspose.Drawing.Imaging.ColorMatrix();

                                // Set alpha (opacity) to 30 % (0.3f).
                                colorMatrix.Matrix33 = 0.3f;

                                imgAttr.SetColorMatrix(
                                    colorMatrix,
                                    Aspose.Drawing.Imaging.ColorMatrixFlag.Default,
                                    Aspose.Drawing.Imaging.ColorAdjustType.Bitmap);

                                // Calculate centered position for the watermark.
                                int wmWidth = watermarkImage.Width;
                                int wmHeight = watermarkImage.Height;
                                int posX = (baseImage.Width - wmWidth) / 2;
                                int posY = (baseImage.Height - wmHeight) / 2;

                                // Draw the watermark onto the base image.
                                graphics.DrawImage(
                                    watermarkImage,
                                    new Aspose.Drawing.Rectangle(posX, posY, wmWidth, wmHeight),
                                    0,
                                    0,
                                    wmWidth,
                                    wmHeight,
                                    Aspose.Drawing.GraphicsUnit.Pixel,
                                    imgAttr);
                            }

                            // Save the watermarked PNG to the output folder.
                            string outputPath = Path.Combine(outputFolder, $"page_{i + 1}.png");
                            baseImage.Save(outputPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}