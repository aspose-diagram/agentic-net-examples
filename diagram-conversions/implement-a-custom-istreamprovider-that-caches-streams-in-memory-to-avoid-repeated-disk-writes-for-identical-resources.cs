using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom IStreamProvider that caches streams in memory.
    public class MemoryCacheStreamProvider : IStreamProvider
    {
        // Cache keyed by the resource path (DefaultPath).
        private readonly Dictionary<string, MemoryStream> _cache = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

        // Called by Aspose when a new stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Use the DefaultPath as the cache key.
            string key = options.DefaultPath ?? string.Empty;

            if (_cache.TryGetValue(key, out MemoryStream existingStream))
            {
                // Reuse the existing memory stream.
                existingStream.Position = 0;
                options.Stream = existingStream;
            }
            else
            {
                // Create a new memory stream and store it in the cache.
                MemoryStream ms = new MemoryStream();
                _cache[key] = ms;
                options.Stream = ms;
            }
        }

        // Called by Aspose after the stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // No disposal here; keep the stream in cache for future reuse.
            // Ensure any buffered data is flushed.
            options.Stream?.Flush();
        }

        // Optional helper to retrieve the cached data for a given path.
        public byte[] GetCachedData(string path)
        {
            if (path == null) return null;
            if (_cache.TryGetValue(path, out MemoryStream ms))
            {
                return ms.ToArray();
            }
            return null;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load a diagram (replace with your actual file path).
                string diagramPath = "sample.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    SaveAsSingleFile = false,
                    StreamProvider = new MemoryCacheStreamProvider()
                };

                // Export the diagram to HTML.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                // Example: retrieve cached stream data for a specific resource (e.g., an image).
                // The key corresponds to the DefaultPath used internally by Aspose during export.
                // Here we just demonstrate how to access the cache; actual keys depend on the export process.
                var provider = (MemoryCacheStreamProvider)htmlOptions.StreamProvider;
                byte[] cachedImage = provider.GetCachedData("image1.png");
                if (cachedImage != null)
                {
                    Console.WriteLine($"Cached image size: {cachedImage.Length} bytes");
                }
                else
                {
                    Console.WriteLine("No cached data found for the specified resource.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}