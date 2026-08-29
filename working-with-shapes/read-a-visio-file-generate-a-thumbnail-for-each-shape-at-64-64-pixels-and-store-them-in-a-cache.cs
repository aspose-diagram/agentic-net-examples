using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the Visio file path as the first argument.
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: VisioThumbnailCache <visio-file-path>");
                    return;
                }

                string visioPath = args[0];

                // Load the Visio diagram.
                Diagram diagram = new Diagram(visioPath);

                // Cache to store thumbnails: key = shape ID, value = PNG byte array.
                Dictionary<long, byte[]> thumbnailCache = new Dictionary<long, byte[]>();

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare image save options for a 64×64 PNG thumbnail.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        imgOptions.PageSize = new PageSize(64, 64);
                        imgOptions.ExportHiddenPage = false;

                        // Render the shape to a memory stream.
                        using (MemoryStream ms = new MemoryStream())
                        {
                            shape.ToImage(ms, imgOptions);
                            thumbnailCache[shape.ID] = ms.ToArray();
                        }
                    }
                }

                // Example usage: write the number of cached thumbnails.
                Console.WriteLine($"Generated thumbnails for {thumbnailCache.Count} shapes.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }