using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MemoryStreamProvider : IStreamProvider
{
    // Stores the generated image streams keyed by their default path.
    public Dictionary<string, MemoryStream> Resources { get; } = new Dictionary<string, MemoryStream>();

    // Called by Aspose when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Create a fresh memory stream for the resource.
        var memoryStream = new MemoryStream();

        // Assign the stream to the options so Aspose can write into it.
        options.Stream = memoryStream;

        // Store the stream using the default path as the key for later processing.
        // DefaultPath is read‑only; we only read its value.
        Resources[options.DefaultPath] = memoryStream;
    }

    // Called by Aspose after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // No additional action required; the stream remains in the dictionary.
        // Aspose will handle flushing/closing as needed.
    }

    // Helper to get the number of collected resources.
    public int ResourceCount => Resources.Count;
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path.
        string outputPath = "output.html";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create HTML save options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Instantiate and assign the custom stream provider.
                MemoryStreamProvider provider = new MemoryStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Save the diagram to HTML; the provider will capture image resources.
                diagram.Save(outputPath, htmlOptions);

                // After saving, report collected image resources.
                Console.WriteLine($"HTML saved to: {outputPath}");
                Console.WriteLine($"Collected {provider.ResourceCount} image resource(s) via MemoryStreamProvider.");

                // Example processing: display each resource's size.
                foreach (var kvp in provider.Resources)
                {
                    string resourcePath = kvp.Key;
                    long size = kvp.Value.Length;
                    Console.WriteLine($"Resource: {resourcePath}, Size: {size} bytes");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}