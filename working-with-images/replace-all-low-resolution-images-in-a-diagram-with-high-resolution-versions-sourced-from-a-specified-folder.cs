using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    // Threshold for low‑resolution images (pixels). Images with width or height below this are considered low‑res.
    const int LowResolutionThreshold = 200;

    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: <program> <diagramPath> <highResFolderPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string highResFolder = args[1];
        string outputPath = args[2];

        // Guard: ensure the source diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the folder containing high‑resolution images exists.
        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution folder not found: {highResFolder}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes (foreign objects).
                    if (shape.Type != TypeValue.Foreign)
                        continue;

                    // Ensure the shape actually contains image data.
                    if (shape.ForeignData == null || shape.ForeignData.Value == null || shape.ForeignData.Value.Length == 0)
                        continue;

                    // Determine image dimensions using Aspose.Drawing.Image.
                    int imgWidth, imgHeight;
                    using (var ms = new MemoryStream(shape.ForeignData.Value))
                    using (var img = Aspose.Drawing.Image.FromStream(ms))
                    {
                        imgWidth = img.Width;
                        imgHeight = img.Height;
                    }

                    // Skip shapes that already meet the resolution threshold.
                    if (imgWidth >= LowResolutionThreshold && imgHeight >= LowResolutionThreshold)
                        continue;

                    // Attempt to locate a high‑resolution replacement file.
                    // Use the shape's name (without extension) to match a file in the folder.
                    string baseName = Path.GetFileNameWithoutExtension(shape.NameU ?? shape.Name ?? "image");
                    string[] possibleExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tif", ".tiff" };
                    string replacementPath = null;

                    foreach (var ext in possibleExtensions)
                    {
                        string candidate = Path.Combine(highResFolder, baseName + ext);
                        if (File.Exists(candidate))
                        {
                            replacementPath = candidate;
                            break;
                        }
                    }

                    // If no matching high‑resolution file is found, log and continue.
                    if (replacementPath == null)
                    {
                        Console.WriteLine($"No high‑resolution image found for shape '{shape.NameU}'. Skipping.");
                        continue;
                    }

                    // Load the high‑resolution image bytes and replace the foreign data.
                    byte[] highResBytes = File.ReadAllBytes(replacementPath);
                    shape.ForeignData.Value = highResBytes;

                    Console.WriteLine($"Replaced image for shape '{shape.NameU}' with '{Path.GetFileName(replacementPath)}'.");
                }
            }

            // Save the updated diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}