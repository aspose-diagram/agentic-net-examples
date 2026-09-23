using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Directory where PNG files will be saved
            string outputDir = "output";
            Directory.CreateDirectory(outputDir);

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Export each page to PNG and apply color inversion
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Export page i to PNG
                string pngPath = Path.Combine(outputDir, $"page_{i + 1}.png");
                var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = i
                };
                diagram.Save(pngPath, imgOptions);

                // Invert colors of the exported PNG
                using (Bitmap bmp = new Bitmap(pngPath))
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            Color original = bmp.GetPixel(x, y);
                            Color inverted = Color.FromArgb(
                                255 - original.R,
                                255 - original.G,
                                255 - original.B);
                            bmp.SetPixel(x, y, inverted);
                        }
                    }

                    // Overwrite the original PNG with the inverted image
                    bmp.Save(pngPath, ImageFormat.Png);
                }
            }

            Console.WriteLine("All pages exported and color-inverted PNG images saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
