using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputZipPath>");
            return;
        }

        string visioPath = args[0];
        string zipPath = args[1];

        // Guard against missing input file
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input file not found: {visioPath}");
            return;
        }

        // Create a temporary directory for extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "VisioImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare a unique file name for the shape image
                        string imageFileName = $"page_{page.ID}_shape_{shape.ID}.png";
                        string imagePath = Path.Combine(tempDir, imageFileName);

                        try
                        {
                            // Export the shape to PNG using Aspose.Diagram
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            shape.ToImage(imagePath, imgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                            continue;
                        }

                        try
                        {
                            // Load the exported image using Aspose.Drawing and convert to grayscale
                            using (Bitmap bitmap = new Bitmap(imagePath))
                            {
                                for (int y = 0; y < bitmap.Height; y++)
                                {
                                    for (int x = 0; x < bitmap.Width; x++)
                                    {
                                        // Get original pixel color
                                        Color original = bitmap.GetPixel(x, y);
                                        // Compute luminance using Rec. 601 formula
                                        int gray = (int)(0.299 * original.R + 0.587 * original.G + 0.114 * original.B);
                                        // Clamp to byte range
                                        gray = Math.Max(0, Math.Min(255, gray));
                                        // Create grayscale color preserving alpha
                                        Color grayColor = Color.FromArgb(original.A, gray, gray, gray);
                                        // Set the pixel back
                                        bitmap.SetPixel(x, y, grayColor);
                                    }
                                }
                                // Overwrite the original image with the grayscale version
                                bitmap.Save(imagePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to convert image to grayscale for shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                        }
                    }
                }
            }

            // Create ZIP archive from the temporary directory
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(tempDir, zipPath);
            Console.WriteLine($"Images extracted and archived to: {zipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up temporary directory
            try
            {
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}