using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MemoryStreamProvider : IStreamProvider
{
    // Stores the generated streams keyed by the default path of the resource.
    private readonly Dictionary<string, MemoryStream> _streams = new Dictionary<string, MemoryStream>();

    // Called by Aspose.Diagram when a resource stream is needed.
    public void InitStream(StreamProviderOptions options)
    {
        // Create a new memory stream for the resource.
        var ms = new MemoryStream();
        // Assign the stream to the options so Aspose can write into it.
        options.Stream = ms;
        // Store the stream using the default path as the key for later retrieval.
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            _streams[options.DefaultPath] = ms;
        }
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // The stream is already stored; optionally flush or reset position.
        if (options.Stream != null)
        {
            options.Stream.Flush();
            options.Stream.Position = 0;
        }
    }

    // Helper to retrieve a generated stream by its resource path.
    public MemoryStream GetStream(string resourcePath)
    {
        return _streams.TryGetValue(resourcePath, out var ms) ? ms : null;
    }

    // Helper to enumerate all stored streams.
    public IEnumerable<KeyValuePair<string, MemoryStream>> GetAllStreams()
    {
        return _streams;
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path).
            string diagramPath = "sample.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Configure HTML export options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            var streamProvider = new MemoryStreamProvider();
            htmlOptions.StreamProvider = streamProvider;

            // Export the diagram to HTML. The output path is required but the actual files
            // will be written to the memory streams provided by the stream provider.
            string outputHtmlPath = "output.html";
            diagram.Save(outputHtmlPath, htmlOptions);

            // After saving, process the in‑memory resources.
            foreach (var entry in streamProvider.GetAllStreams())
            {
                string resourcePath = entry.Key;          // e.g., "images/img1.png"
                MemoryStream ms = entry.Value;

                // Example processing: write the resource to the console as a base64 string.
                byte[] data = ms.ToArray();
                string base64 = Convert.ToBase64String(data);
                Console.WriteLine($"Resource: {resourcePath}, Size: {data.Length} bytes");
                Console.WriteLine($"Base64: {base64}");
            }

            // Clean up.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}