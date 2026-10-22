using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
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

        // Guard: ensure the output folder exists or create it.
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
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare temporary and final BMP file paths.
                    string tempBmpPath = Path.Combine(outputFolder, $"page_{i + 1}_temp.bmp");
                    string finalBmpPath = Path.Combine(outputFolder, $"page_{i + 1}.bmp");

                    // Configure image save options for BMP export.
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Bmp);
                    imgOptions.PageIndex = i;   // Export only the current page.
                    imgOptions.PageCount = 1;   // Single page.

                    // Export the page to a temporary BMP file.
                    diagram.Save(tempBmpPath, imgOptions);

                    // Resize the exported BMP to a fixed width of 800 pixels while preserving aspect ratio.
                    using (Aspose.Drawing.Image original = Aspose.Drawing.Image.FromFile(tempBmpPath))
                    {
                        int originalWidth = original.Width;
                        int originalHeight = original.Height;

                        // Calculate new dimensions.
                        int newWidth = 800;
                        int newHeight = (int)(originalHeight * (newWidth / (double)originalWidth));

                        // Create a new bitmap with the target size.
                        using (Aspose.Drawing.Bitmap resized = new Aspose.Drawing.Bitmap(newWidth, newHeight))
                        {
                            using (Aspose.Drawing.Graphics g = Aspose.Drawing.Graphics.FromImage(resized))
                            {
                                // Use high‑quality scaling.
                                g.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(original, 0, 0, newWidth, newHeight);
                            }

                            // Save the resized image to the final path.
                            resized.Save(finalBmpPath, Aspose.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }

                    // Delete the temporary BMP file.
                    try
                    {
                        File.Delete(tempBmpPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Warning: could not delete temporary file '{tempBmpPath}': {ex.Message}");
                    }

                    Console.WriteLine($"Page {i + 1} exported and resized to '{finalBmpPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
