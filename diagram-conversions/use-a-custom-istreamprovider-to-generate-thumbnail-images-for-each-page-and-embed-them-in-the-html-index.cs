using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramThumbnailHtmlExport
{
    // Custom stream provider that supplies image data for HTML resources
    public class CustomStreamProvider : IStreamProvider
    {
        private readonly Dictionary<string, byte[]> _resources;

        public CustomStreamProvider(Dictionary<string, byte[]> resources)
        {
            _resources = resources ?? new Dictionary<string, byte[]>();
        }

        // Called by Aspose when a resource stream is required
        public void InitStream(StreamProviderOptions options)
        {
            // options.DefaultPath contains the requested resource name (e.g., "page_0.png")
            if (options == null) return;

            if (_resources.TryGetValue(options.DefaultPath, out var data))
            {
                // Provide a fresh memory stream containing the image bytes
                options.Stream = new MemoryStream(data);
            }
            else
            {
                // If the resource is not found, return an empty stream to avoid errors
                options.Stream = Stream.Null;
            }
        }

        // Called after the resource has been written; no special cleanup needed here
        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the stream is disposed if it was created
            options?.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                const string inputPath = "input.vsdx";
                // Path for the generated HTML file
                const string outputHtml = "output.html";

                // Load the diagram
                using (var diagram = new Diagram(inputPath))
                {
                    // Dictionary to hold thumbnail image data keyed by the expected HTML resource name
                    var thumbnailResources = new Dictionary<string, byte[]>();

                    // Generate a thumbnail for each page
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Configure image export options for a small PNG thumbnail
                        var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,
                            PageCount = 1,
                            Scale = 0.2f // 20% of original size
                        };

                        // Export the page to a memory stream
                        using (var ms = new MemoryStream())
                        {
                            diagram.Save(ms, imgOptions);
                            // The HTML exporter expects image files named like "page_0.png", "page_1.png", etc.
                            string resourceName = $"page_{i}.png";
                            thumbnailResources[resourceName] = ms.ToArray();
                        }
                    }

                    // Set up HTML export options and assign the custom stream provider
                    var htmlOptions = new HTMLSaveOptions
                    {
                        StreamProvider = new CustomStreamProvider(thumbnailResources)
                    };

                    // Export the diagram to HTML; the images will be supplied by the stream provider
                    diagram.Save(outputHtml, htmlOptions);
                }

                Console.WriteLine("HTML export with embedded thumbnails completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}