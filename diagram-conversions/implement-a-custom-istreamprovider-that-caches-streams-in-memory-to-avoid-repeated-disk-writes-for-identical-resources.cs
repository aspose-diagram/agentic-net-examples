using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MemoryCacheStreamProvider : IStreamProvider
{
    // Cache to store streams keyed by the resource's default path.
    private readonly Dictionary<string, MemoryStream> _cache = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

    // Initializes the stream for a given resource.
    public void InitStream(StreamProviderOptions options)
    {
        // The DefaultPath identifies the resource (e.g., image file name).
        string path = options.DefaultPath ?? string.Empty;

        // Reuse an existing MemoryStream if the resource was already processed.
        if (!_cache.TryGetValue(path, out MemoryStream memoryStream))
        {
            memoryStream = new MemoryStream();
            _cache[path] = memoryStream;
        }

        // Assign the (cached) stream to the options so Aspose writes into it.
        options.Stream = memoryStream;
    }

    // Called after Aspose finishes writing to the stream.
    public void CloseStream(StreamProviderOptions options)
    {
        // No action needed for in‑memory caching. Optionally reset position.
        if (options.Stream != null)
        {
            options.Stream.Position = 0;
        }
    }

    // Exposes the cached streams for further processing (e.g., writing to disk).
    public IReadOnlyDictionary<string, MemoryStream> GetCachedStreams()
    {
        return _cache;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output HTML file path (second argument).
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML save options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            MemoryCacheStreamProvider streamProvider = new MemoryCacheStreamProvider();
            htmlOptions.StreamProvider = streamProvider;

            // Save the diagram to HTML. The stream provider will cache images/shapes in memory.
            diagram.Save(outputPath, htmlOptions);

            // After saving, write cached resources to a folder next to the HTML file.
            string resourcesDir = Path.Combine(Path.GetDirectoryName(outputPath) ?? string.Empty, "Resources");
            Directory.CreateDirectory(resourcesDir);

            foreach (var kvp in streamProvider.GetCachedStreams())
            {
                string resourceFileName = kvp.Key;
                MemoryStream ms = kvp.Value;

                // Ensure the stream is positioned at the beginning before writing.
                ms.Position = 0;

                string resourcePath = Path.Combine(resourcesDir, resourceFileName);
                using (FileStream fileStream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileStream);
                }

                Console.WriteLine($"Cached resource written: {resourcePath}");
            }

            Console.WriteLine("HTML export completed successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}