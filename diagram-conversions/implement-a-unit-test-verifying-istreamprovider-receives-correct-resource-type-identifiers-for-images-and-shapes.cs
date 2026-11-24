using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TestStreamProvider : IStreamProvider
{
    // Stores the resource type identifier (if available) and the default path for each InitStream call
    public List<(string ResourceType, string DefaultPath)> Calls = new List<(string, string)>();

    public void InitStream(StreamProviderOptions options)
    {
        // TODO: Retrieve the actual resource type identifier from StreamProviderOptions when the API provides it.
        // For now, we use a placeholder value.
        string resourceType = "Unknown";

        // Record the (placeholder) resource type and the default path supplied by Aspose
        string defaultPath = options.DefaultPath ?? string.Empty;
        Calls.Add((resourceType, defaultPath));

        // Provide an in‑memory stream where Aspose will write the resource
        options.Stream = new MemoryStream();
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream after Aspose has finished writing
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "sample.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputPath = "output.html";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                TestStreamProvider provider = new TestStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Save the diagram to HTML using the options
                diagram.Save(outputPath, htmlOptions);

                // After saving, verify that the stream provider received calls for images and shapes
                bool receivedImage = false;
                bool receivedShape = false;

                foreach (var call in provider.Calls)
                {
                    // TODO: Replace the placeholder check with actual resource type comparison when available.
                    // Example (when ResourceType is accessible):
                    // if (call.ResourceType.Contains("Image", StringComparison.OrdinalIgnoreCase)) receivedImage = true;
                    // if (call.ResourceType.Contains("Shape", StringComparison.OrdinalIgnoreCase)) receivedShape = true;

                    // As a fallback, we can attempt to infer from the default path
                    if (!receivedImage && call.DefaultPath.IndexOf("image", StringComparison.OrdinalIgnoreCase) >= 0)
                        receivedImage = true;
                    if (!receivedShape && call.DefaultPath.IndexOf("shape", StringComparison.OrdinalIgnoreCase) >= 0)
                        receivedShape = true;
                }

                // Manual assertions – throw exception if expectations are not met
                if (!receivedImage)
                    throw new Exception("IStreamProvider did not receive any calls for image resources.");
                if (!receivedShape)
                    throw new Exception("IStreamProvider did not receive any calls for shape resources.");

                Console.WriteLine("IStreamProvider received resource calls for images and shapes.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}