using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            int pageIdx = 0;
            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Process each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Load image from byte array using Aspose.Drawing
                        using (MemoryStream msIn = new MemoryStream(imageBytes))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(msIn))
                        using (Aspose.Drawing.Bitmap bmp = new Aspose.Drawing.Bitmap(img))
                        {
                            // Apply grayscale conversion pixel by pixel
                            for (int y = 0; y < bmp.Height; y++)
                            {
                                for (int x = 0; x < bmp.Width; x++)
                                {
                                    Aspose.Drawing.Color pixel = bmp.GetPixel(x, y);
                                    int gray = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                                    gray = Math.Clamp(gray, 0, 255);
                                    Aspose.Drawing.Color grayColor = Aspose.Drawing.Color.FromArgb(gray, gray, gray);
                                    bmp.SetPixel(x, y, grayColor);
                                }
                            }

                            // Save the modified bitmap back to a byte array (PNG format)
                            using (MemoryStream msOut = new MemoryStream())
                            {
                                bmp.Save(msOut, Aspose.Drawing.Imaging.ImageFormat.Png);
                                shape.ForeignData.Value = msOut.ToArray();
                            }
                        }
                    }
                }

                // Export the current page as a TIFF image
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                saveOptions.PageIndex = pageIdx;   // Export only this page
                saveOptions.PageCount = 1;        // Single page export

                string outputPath = Path.Combine(outputFolder, $"Page_{pageIdx + 1}.tiff");
                diagram.Save(outputPath, saveOptions);

                pageIdx++;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}