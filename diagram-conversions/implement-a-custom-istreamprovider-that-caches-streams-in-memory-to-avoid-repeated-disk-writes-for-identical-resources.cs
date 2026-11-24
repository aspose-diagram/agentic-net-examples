using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MemoryCacheStreamProvider : IStreamProvider
{
    // In-memory cache keyed by the resource's default path.
    private readonly Dictionary<string, MemoryStream> _cache = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

    // Called by Aspose when a stream for a resource is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Use the read‑only DefaultPath as the cache key.
        string key = options.DefaultPath ?? string.Empty;

        if (!_cache.TryGetValue(key, out MemoryStream ms))
        {
            // No cached stream – create a new one and store it.
            ms = new MemoryStream();
            _cache[key] = ms;
        }
        else
        {
            // Reset existing stream for reuse.
            ms.Position = 0;
            ms.SetLength(0);
        }

        // Assign the stream back to the options so Aspose writes into it.
        options.Stream = ms;
    }

    // Called by Aspose after writing is finished.
    public void CloseStream(StreamProviderOptions options)
    {
        // No special cleanup required for in‑memory streams.
        // The stream remains in the cache for potential reuse.
    }

    // Helper to retrieve cached data (optional, for demonstration).
    public IEnumerable<KeyValuePair<string, MemoryStream>> GetCachedStreams()
    {
        return _cache;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing output folder.
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            MemoryCacheStreamProvider provider = new MemoryCacheStreamProvider();
            htmlOptions.StreamProvider = provider;

            // Define the main HTML file path.
            string htmlOutputPath = Path.Combine(outputFolder, "output.html");

            // Save the diagram to HTML using the custom provider.
            diagram.Save(htmlOutputPath, htmlOptions);

            // After saving, write cached resources (images, etc.) to disk.
            foreach (var entry in provider.GetCachedStreams())
            {
                // The key may be a relative path; combine with output folder.
                string resourcePath = Path.Combine(outputFolder, entry.Key);

                // Ensure the directory exists.
                string dir = Path.GetDirectoryName(resourcePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // Write the in‑memory stream to the file.
                using (FileStream fileStream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write))
                {
                    entry.Value.Position = 0;
                    entry.Value.CopyTo(fileStream);
                }
            }

            Console.WriteLine($"HTML export completed: {htmlOutputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}