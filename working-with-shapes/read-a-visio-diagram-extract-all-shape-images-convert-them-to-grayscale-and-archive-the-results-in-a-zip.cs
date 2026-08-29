using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output ZIP file.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VisioImageExtractor <inputVisioPath> <outputZipPath>");
            return;
        }

        string inputVisioPath = args[0];
        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"File not found: {inputVisioPath}");
            return;
        }

        string outputZipPath = args[1];

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputVisioPath);

            // Create the ZIP archive.
            using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image (foreign) shapes.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Raw image bytes stored in ForeignData.
                            byte[] imageBytes = shape.ForeignData.Value;
                            if (imageBytes == null || imageBytes.Length == 0)
                                continue;

                            // Load the image using Aspose.Drawing (fully qualified to avoid ambiguity).
                            using (MemoryStream sourceStream = new MemoryStream(imageBytes))
                            using (Aspose.Drawing.Image sourceImage = Aspose.Drawing.Image.FromStream(sourceStream))
                            using (Aspose.Drawing.Bitmap bitmap = new Aspose.Drawing.Bitmap(sourceImage))
                            {
                                // Apply grayscale conversion via a color matrix.
                                ColorMatrix grayMatrix = new ColorMatrix(new float[][]
                                {
                                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                                    new float[] {0, 0, 0, 1, 0},
                                    new float[] {0, 0, 0, 0, 1}
                                });

                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(grayMatrix);

                                // Draw the grayscale image onto the bitmap.
                                using (Graphics graphics = Graphics.FromImage(bitmap))
                                {
                                    Aspose.Drawing.Rectangle rect = new Aspose.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                                    graphics.DrawImage(
                                        bitmap,
                                        rect,
                                        0,
                                        0,
                                        bitmap.Width,
                                        bitmap.Height,
                                        GraphicsUnit.Pixel,
                                        imgAttributes);
                                }

                                // Save the processed image to a memory stream (PNG format).
                                using (MemoryStream pngStream = new MemoryStream())
                                {
                                    bitmap.Save(pngStream, ImageFormat.Png);
                                    pngStream.Position = 0;

                                    // Create a unique entry name for the ZIP.
                                    string entryName = $"Page_{page.ID}_Shape_{shape.ID}.png";
                                    ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName);

                                    using (Stream entryStream = zipEntry.Open())
                                    {
                                        pngStream.CopyTo(entryStream);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Extraction complete. Images saved to '{outputZipPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}