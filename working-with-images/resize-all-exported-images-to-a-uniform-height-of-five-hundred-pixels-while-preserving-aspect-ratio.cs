using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image export options for the current page.
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = i;          // Export only this page.
                    imgOptions.PageCount = 1;
                    imgOptions.ExportHiddenPage = false; // Do not export hidden pages (bool, not BOOL).

                    // Export the page to a memory stream.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        diagram.Save(ms, imgOptions);
                        ms.Position = 0; // Reset stream position for reading.

                        // Load the exported image using Aspose.Drawing (fully qualified to avoid ambiguity).
                        using (Aspose.Drawing.Image srcImage = Aspose.Drawing.Image.FromStream(ms))
                        {
                            // Determine new dimensions while preserving aspect ratio.
                            int originalWidth = srcImage.Width;
                            int originalHeight = srcImage.Height;
                            int targetHeight = 500;
                            int targetWidth = (int)Math.Round((double)originalWidth * targetHeight / originalHeight);

                            // Create a new bitmap with the target size.
                            using (Aspose.Drawing.Bitmap resizedBitmap = new Aspose.Drawing.Bitmap(targetWidth, targetHeight))
                            {
                                // Draw the original image onto the new bitmap, scaling it.
                                using (Aspose.Drawing.Graphics graphics = Aspose.Drawing.Graphics.FromImage(resizedBitmap))
                                {
                                    graphics.DrawImage(srcImage, 0, 0, targetWidth, targetHeight);
                                }

                                // Save the resized image to the output folder.
                                string outputPath = Path.Combine(outputFolder, $"page_{i + 1}.png");
                                resizedBitmap.Save(outputPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}