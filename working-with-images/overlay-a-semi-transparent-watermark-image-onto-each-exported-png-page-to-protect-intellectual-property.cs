using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Drawing2D;
using Aspose.Drawing.Imaging;

// Alias Aspose.Drawing types to avoid conflict with Aspose.Diagram.Image
using ADImage = Aspose.Drawing.Image;
using ADGraphics = Aspose.Drawing.Graphics;
using ADColorMatrix = Aspose.Drawing.Imaging.ColorMatrix;
using ADImageAttributes = Aspose.Drawing.Imaging.ImageAttributes;
using ADRectangle = Aspose.Drawing.Rectangle;
using ADGraphicsUnit = Aspose.Drawing.GraphicsUnit;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file, watermark image and output folder
        string diagramPath = "input.vsdx";
        // Guard: ensure Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string watermarkPath = "watermark.png";
        // Guard: ensure watermark image exists
        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"File not found: {watermarkPath}");
            return;
        }

        string outputFolder = "output";
        // Ensure output directory exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram (Aspose.Diagram operation)
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through each page in the diagram
        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            // Export the current page to a temporary PNG file
            var pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                PageIndex = i,   // zero‑based page index
                PageCount = 1    // export only this page
            };

            string pagePngPath = Path.Combine(outputFolder, $"page_{i + 1}.png");
            try
            {
                // Save page as PNG (Aspose.Diagram operation)
                diagram.Save(pagePngPath, pngOptions);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving PNG for page {i + 1}: {ex.Message}");
                continue;
            }

            try
            {
                // Load base PNG and watermark using Aspose.Drawing
                using (ADImage baseImage = ADImage.FromFile(pagePngPath))
                using (ADImage watermarkImage = ADImage.FromFile(watermarkPath))
                using (ADGraphics graphics = ADGraphics.FromImage(baseImage))
                {
                    // Prepare image attributes with desired opacity (e.g., 30%)
                    float opacity = 0.3f;
                    var colorMatrix = new ADColorMatrix(new float[][]
                    {
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, opacity, 0},
                        new float[] {0, 0, 0, 0, 1}
                    });

                    var imgAttr = new ADImageAttributes();
                    imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);

                    // Center the watermark on the page
                    int x = (baseImage.Width - watermarkImage.Width) / 2;
                    int y = (baseImage.Height - watermarkImage.Height) / 2;

                    // Draw the watermark with the transparency settings
                    graphics.DrawImage(
                        watermarkImage,
                        new ADRectangle(x, y, watermarkImage.Width, watermarkImage.Height),
                        0,
                        0,
                        watermarkImage.Width,
                        watermarkImage.Height,
                        ADGraphicsUnit.Pixel,
                        imgAttr);
                }

                // The base image (with watermark) has been saved back to the same file path
                Console.WriteLine($"Processed page {i + 1} -> {pagePngPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error applying watermark to page {i + 1}: {ex.Message}");
            }
        }

        // Clean up diagram resources
        diagram?.Dispose();

        Console.WriteLine("All pages exported with watermark.");
    }
}