using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MemoryCacheStreamProvider : IStreamProvider
{
    // Cache keyed by the default path of the resource.
    private readonly Dictionary<string, MemoryStream> _cache = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

    // Called when Aspose needs a stream for a particular resource.
    public void InitStream(StreamProviderOptions options)
    {
        // Use the default path as the cache key.
        string key = options.DefaultPath ?? string.Empty;

        if (_cache.TryGetValue(key, out MemoryStream cachedStream))
        {
            // Reuse the existing memory stream.
            // Reset position to the beginning for a fresh write/read.
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

    // Called when Aspose finishes using the stream.
    public void CloseStream(StreamProviderOptions options)
    {
        // No special cleanup required for in‑memory streams.
        // The stream remains in the cache for future reuse.
        // Ensure the stream is flushed.
        options.Stream?.Flush();
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                StreamProvider = new MemoryCacheStreamProvider()
            };

            // Export the diagram to HTML using the in‑memory stream caching.
            diagram.Save("output.html", htmlOptions);

            // Dispose the diagram when done.
            diagram.Dispose();

            Console.WriteLine("Diagram exported to HTML with in‑memory stream caching.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}