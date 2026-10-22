using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vdx> <output_directory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: ensure the input VDX file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Ensure the output directory exists (create if necessary).
        try
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory \"{outputDir}\": {ex.Message}");
            return;
        }

        // Load the Visio diagram and extract embedded images.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify embedded images: shapes of type Foreign.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Retrieve the raw image bytes.
                            byte[] imageData = shape.ForeignData?.Value;
                            if (imageData == null || imageData.Length == 0)
                                continue; // No image data present.

                            // Load the image using Aspose.Drawing to determine its format.
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                {
                                    // Determine file extension based on the image format.
                                    string extension = ".bin"; // Fallback extension.
                                    if (img.RawFormat.Equals(Aspose.Drawing.Imaging.ImageFormat.Png))
                                        extension = ".png";
                                    else if (img.RawFormat.Equals(Aspose.Drawing.Imaging.ImageFormat.Jpeg))
                                        extension = ".jpg";
                                    else if (img.RawFormat.Equals(Aspose.Drawing.Imaging.ImageFormat.Bmp))
                                        extension = ".bmp";
                                    else if (img.RawFormat.Equals(Aspose.Drawing.Imaging.ImageFormat.Gif))
                                        extension = ".gif";
                                    else if (img.RawFormat.Equals(Aspose.Drawing.Imaging.ImageFormat.Tiff))
                                        extension = ".tiff";

                                    // Build a unique file name using the shape ID.
                                    string fileName = $"shape_{shape.ID}{extension}";
                                    string outputPath = Path.Combine(outputDir, fileName);

                                    // Save the extracted image to the output directory.
                                    img.Save(outputPath);
                                    Console.WriteLine($"Extracted image from shape ID {shape.ID} to \"{outputPath}\"");
                                }
                            }
                        }
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