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
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputZipPath>");
            return;
        }

        string inputPath = args[0];
        string outputZipPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary directory for extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "VisioShapeImages_" + Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape image
                        string imageFileName = $"Page{page.ID}_Shape{shape.ID}.png";
                        string imagePath = Path.Combine(tempDir, imageFileName);

                        // Export the shape to PNG
                        try
                        {
                            shape.ToImage(imagePath, new ImageSaveOptions(SaveFileFormat.Png));
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                            continue;
                        }

                        // Convert the exported PNG to grayscale using Aspose.Drawing
                        try
                        {
                            using (Aspose.Drawing.Image original = Aspose.Drawing.Image.FromFile(imagePath))
                            {
                                using (Aspose.Drawing.Bitmap grayBitmap = new Aspose.Drawing.Bitmap(original.Width, original.Height))
                                {
                                    using (Aspose.Drawing.Graphics graphics = Aspose.Drawing.Graphics.FromImage(grayBitmap))
                                    {
                                        // Define a grayscale color matrix
                                        float[][] matrixItems = new float[][]
                                        {
                                            new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                                            new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                                            new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                                            new float[] {0, 0, 0, 1, 0},
                                            new float[] {0, 0, 0, 0, 1}
                                        };
                                        Aspose.Drawing.Imaging.ColorMatrix colorMatrix = new Aspose.Drawing.Imaging.ColorMatrix(matrixItems);
                                        Aspose.Drawing.Imaging.ImageAttributes imgAttr = new Aspose.Drawing.Imaging.ImageAttributes();
                                        imgAttr.SetColorMatrix(colorMatrix);

                                        // Draw the original image onto the bitmap using the grayscale matrix
                                        graphics.DrawImage(
                                            original,
                                            new Aspose.Drawing.Rectangle(0, 0, original.Width, original.Height),
                                            0,
                                            0,
                                            original.Width,
                                            original.Height,
                                            Aspose.Drawing.GraphicsUnit.Pixel,
                                            imgAttr);
                                    }

                                    // Overwrite the original file with the grayscale image
                                    grayBitmap.Save(imagePath, Aspose.Drawing.Imaging.ImageFormat.Png);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to convert image to grayscale for shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                        }
                    }
                }
            }

            // Create ZIP archive of all grayscale images
            try
            {
                using (FileStream zipToOpen = new FileStream(outputZipPath, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        foreach (string filePath in Directory.GetFiles(tempDir))
                        {
                            string entryName = Path.GetFileName(filePath);
                            archive.CreateEntryFromFile(filePath, entryName);
                        }
                    }
                }

                Console.WriteLine($"Images successfully archived to: {outputZipPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create ZIP archive: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
        finally
        {
            // Clean up temporary directory
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}
