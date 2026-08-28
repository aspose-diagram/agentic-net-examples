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
        // Expect two arguments: input Visio file and output folder for images
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputFolder = args[1];
        // Guard: create output folder if it does not exist
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Configure image export options for PNG format
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page
                    PageIndex = pageIndex,
                    PageCount = 1
                };

                // Temporary file path for the raw exported image
                string tempImagePath = Path.Combine(Path.GetTempPath(), $"page_{pageIndex}.png");

                // Export the page to a PNG file
                diagram.Save(tempImagePath, imgOptions);

                // Load the exported PNG using Aspose.Drawing
                using (Bitmap bitmap = new Bitmap(tempImagePath))
                {
                    // Apply sepia tone to each pixel
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Retrieve original pixel color
                            Color original = bitmap.GetPixel(x, y);
                            double r = original.R;
                            double g = original.G;
                            double b = original.B;

                            // Compute sepia values
                            int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                            int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                            int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                            // Clamp values to valid byte range
                            tr = Math.Min(255, tr);
                            tg = Math.Min(255, tg);
                            tb = Math.Min(255, tb);

                            // Set the new sepia pixel
                            Color sepia = Color.FromArgb(tr, tg, tb);
                            bitmap.SetPixel(x, y, sepia);
                        }
                    }

                    // Final output path for the sepia‑toned image
                    string finalImagePath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.png");

                    // Save the processed image back to PNG
                    bitmap.Save(finalImagePath, ImageFormat.Png);
                }

                // Delete the temporary raw image
                try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }
            }

            Console.WriteLine("Sepia‑toned image export completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any Aspose or IO errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}