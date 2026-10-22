using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MemoryStreamProvider : IStreamProvider
{
    // Stores the memory streams for each resource path.
    public Dictionary<string, MemoryStream> Streams { get; } = new Dictionary<string, MemoryStream>();

    // Called by Aspose.Diagram when a new resource stream is needed.
    public void InitStream(StreamProviderOptions options)
    {
        // Create a fresh memory stream for the resource.
        var ms = new MemoryStream();

        // Assign the stream to the options so Aspose can write into it.
        options.Stream = ms;

        // Use the default path (read‑only) as a key to retrieve the stream later.
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            Streams[options.DefaultPath] = ms;
        }
    }

    // Called when Aspose.Diagram finishes writing to the resource stream.
    public void CloseStream(StreamProviderOptions options)
    {
        // Reset position for potential downstream processing.
        if (options.Stream != null)
        {
            options.Stream.Position = 0;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create the custom stream provider.
            var streamProvider = new MemoryStreamProvider();

            // Configure HTML save options and assign the provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = streamProvider;

            // Save the diagram to an in‑memory HTML stream.
            using (MemoryStream htmlStream = new MemoryStream())
            {
                diagram.Save(htmlStream, htmlOptions);

                // Optionally write the generated HTML to a file for verification.
                File.WriteAllBytes("output.html", htmlStream.ToArray());
            }

            // After saving, the provider contains all image resources in memory.
            Console.WriteLine("Image resources captured by MemoryStreamProvider:");
            foreach (var kvp in streamProvider.Streams)
            {
                Console.WriteLine($"Resource Path: {kvp.Key}, Size: {kvp.Value.Length} bytes");
                // Example of further processing: you could read the bytes here.
                // byte[] imageData = kvp.Value.ToArray();
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}