using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MemoryStreamProvider : IStreamProvider
{
    // Stores the generated streams keyed by the resource path (e.g., image file name)
    public Dictionary<string, MemoryStream> Streams { get; } = new Dictionary<string, MemoryStream>();

    // Called by Aspose.Diagram before writing a resource (image, CSS, etc.)
    public void InitStream(StreamProviderOptions options)
    {
        // Create a fresh memory stream for the resource
        var ms = new MemoryStream();
        options.Stream = ms;

        // Keep a reference using the default path as the key (if provided)
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            Streams[options.DefaultPath] = ms;
        }
    }

    // Called after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure the stream is ready for reading later
        if (options.Stream != null)
        {
            options.Stream.Position = 0;
        }
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string diagramPath = "sample.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Configure HTML export options and assign the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            var streamProvider = new MemoryStreamProvider();
            htmlOptions.StreamProvider = streamProvider;

            // Export to HTML (the actual HTML file is written to disk, but images are captured in memory)
            string htmlOutputPath = "output.html";
            diagram.Save(htmlOutputPath, htmlOptions);

            // After saving, process the captured image streams as needed
            foreach (var kvp in streamProvider.Streams)
            {
                string resourcePath = kvp.Key;          // e.g., "image1.png"
                MemoryStream ms = kvp.Value;            // image data in memory

                // Example: display the size of each image resource
                Console.WriteLine($"Resource: {resourcePath}, Size: {ms.Length} bytes");

                // If further processing is required, the stream can be read here.
                // For demonstration, we could save the image to disk:
                // File.WriteAllBytes(resourcePath, ms.ToArray());
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}