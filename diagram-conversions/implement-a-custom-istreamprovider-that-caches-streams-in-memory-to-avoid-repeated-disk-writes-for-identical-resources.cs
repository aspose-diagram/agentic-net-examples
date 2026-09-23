using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom IStreamProvider that caches streams in memory
    public class MemoryCacheStreamProvider : IStreamProvider
    {
        // Internal cache: key is the resource path, value is the memory stream containing the data
        private readonly Dictionary<string, MemoryStream> _cache = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

        // Called by Aspose.Diagram when a new stream is required for a resource
        public void InitStream(StreamProviderOptions options)
        {
            // Use the default path as the cache key
            string key = options.DefaultPath ?? Guid.NewGuid().ToString();

            // Reuse an existing stream if the resource was already written before
            if (!_cache.TryGetValue(key, out MemoryStream memStream))
            {
                memStream = new MemoryStream();
                _cache[key] = memStream;
            }

            // Assign the stream to the options so Aspose can write into it
            options.Stream = memStream;
        }

        // Called after the stream has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // No special cleanup required for in‑memory streams
            // Optionally reset the position for later reading
            if (options.Stream != null)
            {
                options.Stream.Position = 0;
            }
        }

        // Helper to retrieve cached data after the diagram has been saved
        public Dictionary<string, byte[]> GetCachedResources()
        {
            var result = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
            foreach (var kvp in _cache)
            {
                // Ensure the stream is at the beginning before reading
                kvp.Value.Position = 0;
                result[kvp.Key] = kvp.Value.ToArray();
            }
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram (replace with an actual file path)
                string sourceDiagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourceDiagramPath);

                // Prepare HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                MemoryCacheStreamProvider streamProvider = new MemoryCacheStreamProvider();
                htmlOptions.StreamProvider = streamProvider;

                // Export the diagram to HTML
                string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                // After saving, write cached resources (e.g., images) to disk for inspection
                var cachedResources = streamProvider.GetCachedResources();
                foreach (var kvp in cachedResources)
                {
                    // The key may be a relative path like "images/image1.png"
                    string resourcePath = Path.Combine("ExportedResources", kvp.Key);
                    string directory = Path.GetDirectoryName(resourcePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    File.WriteAllBytes(resourcePath, kvp.Value);
                    Console.WriteLine($"Saved cached resource: {resourcePath}");
                }

                Console.WriteLine("HTML export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}