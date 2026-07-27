using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that caches streams in memory.
    public class MemoryCacheStreamProvider : IStreamProvider
    {
        // Cache keyed by the default path of the resource.
        private readonly Dictionary<string, MemoryStream> _cache = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

        // Called by Aspose.Diagram when a new stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            // Use the DefaultPath as the key. It is read‑only, so we only read it.
            string key = options.DefaultPath ?? string.Empty;

            if (_cache.TryGetValue(key, out MemoryStream cachedStream))
            {
                // Reuse the existing memory stream.
                // Reset position to the beginning for a fresh write.
                cachedStream.Position = 0;
                options.Stream = cachedStream;
            }
            else
            {
                // Create a new memory stream and store it in the cache.
                MemoryStream newStream = new MemoryStream();
                _cache[key] = newStream;
                options.Stream = newStream;
            }
        }

        // Called by Aspose.Diagram after the stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            // No action required for in‑memory streams.
            // The cached streams remain available for future InitStream calls.
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram (replace with your actual file path).
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new MemoryCacheStreamProvider(),
                    // Optional: export all pages as a single HTML file.
                    SaveAsSingleFile = true
                };

                // Export the diagram to HTML. The HTML files (or a single file) will be written to memory.
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                // At this point the HTML content resides in the memory cache.
                // Example: retrieve the generated HTML from the provider's cache.
                // (In a real scenario you might write the cached stream to disk or use it directly.)
                Console.WriteLine("HTML export completed. Streams are cached in memory.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}