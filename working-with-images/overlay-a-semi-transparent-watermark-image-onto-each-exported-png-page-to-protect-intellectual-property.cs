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
        // Expected arguments: <diagramPath> <outputFolder> <watermarkImagePath>
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <outputFolder> <watermarkImagePath>");
            return;
        }

        string diagramPath = args[0];
        string outputFolder = args[1];
        string watermarkPath = args[2];

        // Guard: check diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: check watermark image exists
        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image file not found: {watermarkPath}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load the watermark image once (assumed to have desired transparency)
                using (Aspose.Drawing.Image watermarkImage = Aspose.Drawing.Image.FromFile(watermarkPath))
                {
                    // Iterate through each page in the diagram
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Prepare PNG export options for the current page
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        pngOptions.PageIndex = i;   // zero‑based page index
                        pngOptions.PageCount = 1;   // export only this page

                        // Define temporary and final PNG file paths
                        string tempPngPath = Path.Combine(outputFolder, $"page_{i + 1}_temp.png");
                        string finalPngPath = Path.Combine(outputFolder, $"page_{i + 1}.png");

                        // Export the page to a temporary PNG file
                        diagram.Save(tempPngPath, pngOptions);

                        // Load the exported PNG and overlay the watermark
                        using (Aspose.Drawing.Image baseImage = Aspose.Drawing.Image.FromFile(tempPngPath))
                        {
                            // Create graphics object to draw the watermark
                            using (Aspose.Drawing.Graphics graphics = Aspose.Drawing.Graphics.FromImage(baseImage))
                            {
                                // Draw the watermark over the entire page.
                                // The watermark image should already contain semi‑transparent pixels.
                                graphics.DrawImage(
                                    watermarkImage,
                                    new RectangleF(0, 0, baseImage.Width, baseImage.Height));
                            }

                            // Save the final image with the watermark
                            baseImage.Save(finalPngPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                        }

                        // Delete the temporary PNG file
                        try
                        {
                            File.Delete(tempPngPath);
                        }
                        catch
                        {
                            // If deletion fails, ignore – the temp file will be left on disk.
                        }

                        Console.WriteLine($"Exported page {i + 1} with watermark to: {finalPngPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
