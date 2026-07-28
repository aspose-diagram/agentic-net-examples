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
        try
        {

            // Input Visio file path (adjust as needed)
            string visioPath = "input.vsdx";

            // Output folder for PNG images
            string outputFolder = "ExportedImages";
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Prepare PNG export options for the current page
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = pageIndex;   // Export only this page
                pngOptions.PageCount = 1;           // Single page per image

                // Define the PNG file name
                string pngPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.png");

                // Export the page to PNG
                diagram.Save(pngPath, pngOptions);

                // Load the exported PNG using Aspose.Drawing
                using (Bitmap bitmap = new Bitmap(pngPath))
                {
                    // Invert colors pixel by pixel
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            Color original = bitmap.GetPixel(x, y);
                            // Invert each RGB channel; preserve alpha
                            Color inverted = Color.FromArgb(
                                original.A,
                                255 - original.R,
                                255 - original.G,
                                255 - original.B);
                            bitmap.SetPixel(x, y, inverted);
                        }
                    }

                    // Save the inverted image, overwriting the original PNG
                    bitmap.Save(pngPath, ImageFormat.Png);
                }

                Console.WriteLine($"Exported and inverted page {pageIndex + 1} to {pngPath}");
            }

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
