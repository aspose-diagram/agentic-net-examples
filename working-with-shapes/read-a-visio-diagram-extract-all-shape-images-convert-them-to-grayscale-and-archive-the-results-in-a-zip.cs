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
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputZipPath = args.Length > 1 ? args[1] : "images.zip";

        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        try
        {
            using (var zipStream = new FileStream(outputZipPath, FileMode.Create))
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                int imageCounter = 0;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Type != TypeValue.Foreign)
                            continue;

                        try
                        {
                            string tempPngPath = Path.Combine(Path.GetTempPath(),
                                $"shape_{imageCounter}_{Guid.NewGuid()}.png");

                            var imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            shape.ToImage(tempPngPath, imgOptions);

                            using (var originalImage = Aspose.Drawing.Image.FromFile(tempPngPath))
                            {
                                float[][] matrixElements =
                                {
                                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                                    new float[] {0, 0, 0, 1, 0},
                                    new float[] {0, 0, 0, 0, 1}
                                };
                                var colorMatrix = new ColorMatrix(matrixElements);
                                var imgAttr = new ImageAttributes();
                                imgAttr.SetColorMatrix(colorMatrix);

                                using (var grayBitmap = new Bitmap(originalImage.Width, originalImage.Height))
                                using (var graphics = Graphics.FromImage(grayBitmap))
                                {
                                    graphics.DrawImage(originalImage, new Rectangle(0, 0, grayBitmap.Width, grayBitmap.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, imgAttr);
                                    using (var ms = new MemoryStream())
                                    {
                                        grayBitmap.Save(ms, ImageFormat.Png);
                                        ms.Position = 0;
                                        var entry = archive.CreateEntry($"image_{imageCounter}.png");
                                        using (var entryStream = entry.Open())
                                        {
                                            ms.CopyTo(entryStream);
                                        }
                                    }
                                }
                            }

                            File.Delete(tempPngPath);
                            imageCounter++;
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating ZIP archive: {ex.Message}");
        }
    }
}