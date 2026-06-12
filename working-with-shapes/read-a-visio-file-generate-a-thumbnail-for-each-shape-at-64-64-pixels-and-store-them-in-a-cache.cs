using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (first argument or default)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Cache to store thumbnails: key = shape ID, value = image bytes (PNG)
            Dictionary<long, byte[]> thumbnailCache = new Dictionary<long, byte[]>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare image options for a 64x64 PNG thumbnail
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageSize = new PageSize(64f, 64f); // width, height in pixels (as float)

                    // Export shape to a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        shape.ToImage(ms, imgOptions);
                        thumbnailCache[shape.ID] = ms.ToArray();
                    }
                }
            }

            // Simple verification output
            Console.WriteLine($"Generated thumbnails for {thumbnailCache.Count} shapes.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
