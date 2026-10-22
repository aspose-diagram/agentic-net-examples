using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

// Alias Aspose.Drawing types for clarity
using AsposeImage = Aspose.Drawing.Image;
using AsposeBitmap = Aspose.Drawing.Bitmap;
using AsposeColor = Aspose.Drawing.Color;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Configure image save options for PNG export (single page)
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.PageIndex = pageIndex;   // zero‑based page index
                imgOptions.PageCount = 1;           // export only this page

                // Export the page to a memory stream
                using (MemoryStream pngStream = new MemoryStream())
                {
                    diagram.Save(pngStream, imgOptions);
                    pngStream.Position = 0; // reset stream position for reading

                    // Load the PNG into Aspose.Drawing.Bitmap for pixel manipulation
                    using (AsposeBitmap bitmap = new AsposeBitmap(pngStream))
                    {
                        // Invert colors pixel by pixel
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                AsposeColor original = bitmap.GetPixel(x, y);
                                // Compute inverted color (RGB components)
                                int invR = 255 - original.R;
                                int invG = 255 - original.G;
                                int invB = 255 - original.B;
                                // Preserve original alpha
                                AsposeColor inverted = AsposeColor.FromArgb(original.A, invR, invG, invB);
                                bitmap.SetPixel(x, y, inverted);
                            }
                        }

                        // Build output file path
                        string outputPath = Path.Combine(outputFolder, $"page_{pageIndex + 1}_negative.png");

                        // Save the inverted bitmap as PNG
                        bitmap.Save(outputPath, ImageFormat.Png);
                        Console.WriteLine($"Saved negative image: {outputPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}