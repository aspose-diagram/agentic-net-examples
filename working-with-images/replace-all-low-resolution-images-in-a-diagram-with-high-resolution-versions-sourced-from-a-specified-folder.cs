using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input diagram path, folder with high‑resolution images, output diagram path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: DiagramImageReplacement <input.vsdx> <highResFolder> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string highResFolder = args[1];
        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution folder not found: {highResFolder}");
            return;
        }

        string outputPath = args[2];

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            ReplaceLowResolutionImages(diagram, highResFolder);

            // Save the updated diagram (using VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Scans all pages and shapes, identifies foreign (image) shapes,
    /// checks their pixel dimensions, and replaces them with higher‑resolution
    /// images from the supplied folder when a matching file is found.
    /// </summary>
    /// <param name="diagram">The loaded diagram.</param>
    /// <param name="highResFolder">Folder containing high‑resolution images.</param>
    private static void ReplaceLowResolutionImages(Diagram diagram, string highResFolder)
    {
        const int minWidth = 500;
        const int minHeight = 500;

        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only process foreign (image) shapes
                    if (shape.Type != TypeValue.Foreign)
                        continue;

                    // Ensure the shape actually contains image data
                    if (shape.ForeignData == null || shape.ForeignData.Value == null)
                        continue;

                    // Determine current image dimensions
                    int imgWidth, imgHeight;
                    using (var lowStream = new MemoryStream(shape.ForeignData.Value))
                    using (var lowImg = Aspose.Drawing.Image.FromStream(lowStream))
                    {
                        imgWidth = lowImg.Width;
                        imgHeight = lowImg.Height;
                    }

                    // Skip if image meets the minimum size
                    if (imgWidth >= minWidth && imgHeight >= minHeight)
                        continue;

                    // Locate a high‑resolution image file using the shape's name
                    string baseName = shape.NameU ?? shape.Name ?? $"shape_{shape.ID}";
                    string highResPath = FindImageFile(highResFolder, baseName);

                    if (highResPath == null)
                    {
                        Console.WriteLine($"High‑resolution image not found for shape '{baseName}'. Skipping.");
                        continue;
                    }

                    // Replace image data
                    byte[] highResBytes = File.ReadAllBytes(highResPath);
                    shape.ForeignData.Value = highResBytes;
                    Console.WriteLine($"Replaced image for shape '{baseName}' with '{Path.GetFileName(highResPath)}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error replacing images: {ex.Message}");
        }
    }

    /// <summary>
    /// Searches the specified folder for an image file matching the base name.
    /// Supports common image extensions.
    /// </summary>
    /// <param name="folder">Folder to search.</param>
    /// <param name="baseName">Base file name without extension.</param>
    /// <returns>Full path to the image file if found; otherwise null.</returns>
    private static string FindImageFile(string folder, string baseName)
    {
        string[] extensions = { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tiff" };
        foreach (string ext in extensions)
        {
            string candidate = Path.Combine(folder, baseName + ext);
            if (File.Exists(candidate))
                return candidate;
        }
        return null;
    }
}