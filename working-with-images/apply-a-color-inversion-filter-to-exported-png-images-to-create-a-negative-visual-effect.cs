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

            // Input Visio file
            string inputPath = "input.vsdx";

            // Directory to store the negative PNG images
            string outputDir = "output";
            Directory.CreateDirectory(outputDir);

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            int pageCount = diagram.Pages.Count;
            for (int i = 0; i < pageCount; i++)
            {
                // Export the current page to a PNG image in memory
                using (MemoryStream ms = new MemoryStream())
                {
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = i;   // Export specific page
                    imgOptions.PageCount = 1;   // Export only one page

                    diagram.Save(ms, imgOptions);
                    ms.Position = 0;

                    // Load the PNG image using Aspose.Drawing
                    using (Bitmap bmp = new Bitmap(ms))
                    {
                        // Invert colors pixel by pixel
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                Color original = bmp.GetPixel(x, y);
                                Color inverted = Color.FromArgb(
                                    original.A,
                                    255 - original.R,
                                    255 - original.G,
                                    255 - original.B);
                                bmp.SetPixel(x, y, inverted);
                            }
                        }

                        // Save the negative image to the output directory
                        string outPath = Path.Combine(outputDir, $"page_{i + 1}_negative.png");
                        bmp.Save(outPath, ImageFormat.Png);
                    }
                }
            }

            diagram.Dispose();
            Console.WriteLine("All pages exported with negative color effect.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
