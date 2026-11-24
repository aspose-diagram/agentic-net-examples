using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists or create it
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
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare image export options for a single page
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = i;      // export current page
                    imgOptions.PageCount = 1;      // only this page
                    imgOptions.ExportHiddenPage = false; // do not export hidden pages

                    // Temporary file path for the exported image
                    string tempImagePath = Path.Combine(outputFolder, $"page_{i + 1}_temp.png");

                    // Export the page to a PNG image
                    diagram.Save(tempImagePath, imgOptions);

                    // Load the exported image using Aspose.Drawing (fully qualified to avoid ambiguity)
                    using (Aspose.Drawing.Image originalImage = Aspose.Drawing.Image.FromFile(tempImagePath))
                    {
                        // Desired uniform height in pixels
                        const int targetHeight = 500;

                        // Calculate scaling factor to preserve aspect ratio
                        double scaleFactor = (double)targetHeight / originalImage.Height;
                        int targetWidth = (int)Math.Round(originalImage.Width * scaleFactor);

                        // Create a new bitmap with the target dimensions
                        using (Aspose.Drawing.Bitmap resizedBitmap = new Aspose.Drawing.Bitmap(targetWidth, targetHeight))
                        {
                            // Set high-quality interpolation mode
                            using (Aspose.Drawing.Graphics graphics = Aspose.Drawing.Graphics.FromImage(resizedBitmap))
                            {
                                graphics.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);
                            }

                            // Final output path (overwrite the temporary file)
                            string finalImagePath = Path.Combine(outputFolder, $"page_{i + 1}.png");
                            resizedBitmap.Save(finalImagePath, Aspose.Drawing.Imaging.ImageFormat.Png);
                        }
                    }

                    // Delete the temporary image file
                    try
                    {
                        File.Delete(tempImagePath);
                    }
                    catch
                    {
                        // If deletion fails, ignore; the temp file will remain but does not affect processing
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