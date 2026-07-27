using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Keep track of created streams to ensure they are disposed.
    private readonly Dictionary<string, Stream> _streams = new();

    // Called by Aspose.Diagram before writing a resource (e.g., an image) to a stream.
    public void InitStream(StreamProviderOptions options)
    {
        // Create a new memory stream for the resource.
        var stream = new MemoryStream();
        // Assign the stream to the options so Aspose can write into it.
        options.Stream = stream;
        // Store the stream using the default path as a key for later disposal.
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            _streams[options.DefaultPath] = stream;
        }
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Retrieve the stream that was used.
        var stream = options.Stream;
        if (stream != null)
        {
            // Ensure the stream is flushed and disposed.
            stream.Flush();
            stream.Dispose();
        }

        // Remove the entry from the tracking dictionary.
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            _streams.Remove(options.DefaultPath);
        }
    }

    // Optional helper to clean up any streams that might not have been closed.
    public void Cleanup()
    {
        foreach (var kvp in _streams)
        {
            kvp.Value?.Dispose();
        }
        _streams.Clear();
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Input folder containing Visio files.
            string inputFolder = @"C:\Visio\Input";
            // Output folder for generated HTML files.
            string outputFolder = @"C:\Visio\Output";

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Get all Visio files (VSDX) in the input folder.
            string[] files = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in files)
            {
                // Load the diagram from file.
                Diagram diagram = new Diagram(filePath);

                // Prepare HTML save options with a custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Assign the custom provider to handle resource streams.
                    StreamProvider = new MyStreamProvider()
                };

                // Determine output HTML file path.
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                // Save the diagram as HTML.
                diagram.Save(outputPath, htmlOptions);

                // Dispose the diagram to free unmanaged resources.
                diagram.Dispose();

                // If the provider implements cleanup (in case any streams were not closed),
                // invoke it to guarantee no leaks.
                if (htmlOptions.StreamProvider is MyStreamProvider provider)
                {
                    provider.Cleanup();
                }

                Console.WriteLine($"Converted '{filePath}' to HTML successfully.");
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}