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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = pageIndex;   // Export only this page.
                pngOptions.PageCount = 1;

                // Define the output PNG file path.
                string pngPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.png");

                // Export the page to PNG.
                diagram.Save(pngPath, pngOptions);

                // Load the exported PNG using Aspose.Drawing.
                using (Bitmap bitmap = new Bitmap(pngPath))
                {
                    // Invert colors pixel by pixel.
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            Color original = bitmap.GetPixel(x, y);
                            int invR = 255 - original.R;
                            int invG = 255 - original.G;
                            int invB = 255 - original.B;
                            Color inverted = Color.FromArgb(original.A, invR, invG, invB);
                            bitmap.SetPixel(x, y, inverted);
                        }
                    }

                    // Save the inverted image back to the same file.
                    bitmap.Save(pngPath, ImageFormat.Png);
                }

                Console.WriteLine($"Processed page {pageIndex + 1}: {pngPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}