using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // In‑memory cache: key = shape ID, value = PNG thumbnail bytes (64 × 64)
    private static readonly Dictionary<long, byte[]> _thumbnailCache = new Dictionary<long, byte[]>();

    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare image save options for a 64 × 64 PNG thumbnail
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageSize = new PageSize(64f, 64f); // width, height in pixels (as float)

                    // Export shape to a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        shape.ToImage(ms, imgOptions);
                        // Store the thumbnail bytes in the cache
                        _thumbnailCache[shape.ID] = ms.ToArray();
                    }
                }
            }

            // Example usage: write the first cached thumbnail to a file (optional)
            if (_thumbnailCache.Count > 0)
            {
                long firstShapeId = 0;
                foreach (var kvp in _thumbnailCache)
                {
                    firstShapeId = kvp.Key;
                    break;
                }

                File.WriteAllBytes("thumbnail_" + firstShapeId + ".png", _thumbnailCache[firstShapeId]);
                Console.WriteLine($"Thumbnail for shape ID {firstShapeId} saved to disk.");
            }
            else
            {
                Console.WriteLine("No shapes found to generate thumbnails.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
