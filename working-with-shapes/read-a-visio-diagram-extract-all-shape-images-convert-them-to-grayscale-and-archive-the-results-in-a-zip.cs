using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeColor = Aspose.Drawing.Color;
using AsposeImageFormat = Aspose.Drawing.Imaging.ImageFormat;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output ZIP file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VisioImageExtractor <inputVisioPath> <outputZipPath>");
            return;
        }

        string inputVisioPath = args[0];
        // Guard to ensure the input file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"File not found: {inputVisioPath}");
            return;
        }

        string outputZipPath = args[1];

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Create the ZIP archive for the extracted images
            using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image (foreign) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            byte[] imageData = shape.ForeignData.Value;
                            if (imageData == null || imageData.Length == 0)
                                continue;

                            // Load the image from the byte array using Aspose.Drawing
                            using (MemoryStream imgStream = new MemoryStream(imageData))
                            using (AsposeImage originalImage = AsposeImage.FromStream(imgStream))
                            using (AsposeBitmap bitmap = new AsposeBitmap(originalImage))
                            {
                                // Convert each pixel to grayscale
                                for (int y = 0; y < bitmap.Height; y++)
                                {
                                    for (int x = 0; x < bitmap.Width; x++)
                                    {
                                        AsposeColor pixel = bitmap.GetPixel(x, y);
                                        int gray = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                                        AsposeColor grayColor = AsposeColor.FromArgb(pixel.A, gray, gray, gray);
                                        bitmap.SetPixel(x, y, grayColor);
                                    }
                                }

                                // Save the grayscale image to a memory stream (PNG format)
                                using (MemoryStream outStream = new MemoryStream())
                                {
                                    bitmap.Save(outStream, AsposeImageFormat.Png);
                                    outStream.Position = 0;

                                    // Create a ZIP entry name that includes page and shape IDs
                                    string entryName = $"Page_{page.ID}_Shape_{shape.ID}.png";
                                    ZipArchiveEntry entry = archive.CreateEntry(entryName);

                                    // Write the image data into the ZIP entry
                                    using (Stream entryStream = entry.Open())
                                    {
                                        outStream.CopyTo(entryStream);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Extraction complete. Images saved to: {outputZipPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}