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
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary).
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Prepare PNG export options for the current page.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = pageIndex,   // Export only this page.
                    PageCount = 1            // Single‑page export.
                };

                // Define temporary and final file paths.
                string tempPngPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");
                string invertedPngPath = Path.Combine(outputFolder, $"page_{pageIndex}_inverted.png");

                // Export the page to a temporary PNG file.
                diagram.Save(tempPngPath, pngOptions);

                // Load the exported PNG using Aspose.Drawing.
                using (Bitmap bitmap = new Bitmap(tempPngPath))
                {
                    // Invert each pixel's RGB channels while preserving alpha.
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            Color original = bitmap.GetPixel(x, y);
                            Color inverted = Color.FromArgb(
                                original.A,
                                255 - original.R,
                                255 - original.G,
                                255 - original.B);
                            bitmap.SetPixel(x, y, inverted);
                        }
                    }

                    // Save the inverted image to the final path.
                    bitmap.Save(invertedPngPath, ImageFormat.Png);
                }

                // Optionally delete the temporary PNG.
                try { File.Delete(tempPngPath); } catch { /* ignore cleanup errors */ }

                Console.WriteLine($"Inverted PNG saved: {invertedPngPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}