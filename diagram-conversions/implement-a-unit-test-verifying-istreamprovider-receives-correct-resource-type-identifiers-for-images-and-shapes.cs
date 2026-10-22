using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Store captured identifiers for verification
    private readonly List<string> _capturedIdentifiers = new List<string>();
    public IReadOnlyList<string> CapturedIdentifiers => _capturedIdentifiers;

    // Called by Aspose when a new resource stream is required
    public void InitStream(StreamProviderOptions options)
    {
        // Attempt to capture a meaningful identifier.
        // Prefer a hypothetical ResourceType property; fall back to DefaultPath.
        string identifier = null;

        // Use reflection to safely access a possible ResourceType property.
        var resourceTypeProp = options.GetType().GetProperty("ResourceType");
        if (resourceTypeProp != null)
        {
            var rtValue = resourceTypeProp.GetValue(options);
            if (rtValue != null)
                identifier = rtValue.ToString();
        }

        // If ResourceType is not available, use the default path provided by Aspose.
        if (string.IsNullOrEmpty(identifier))
        {
            identifier = options.DefaultPath ?? "unknown";
        }

        _capturedIdentifiers.Add(identifier);

        // Provide a dummy stream so Aspose can write the resource.
        // The Stream property is writable in the current API version.
        options.Stream = new MemoryStream();
    }

    // Called by Aspose when the resource stream should be closed
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream we created in InitStream.
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
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

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Instantiate the custom stream provider
            MyStreamProvider provider = new MyStreamProvider();

            // Configure HTML save options with the stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = provider;

            // Prepare output directory and file
            string outputDir = Path.Combine(Path.GetDirectoryName(inputPath) ?? ".", "html_output");
            Directory.CreateDirectory(outputDir);
            string outputPath = Path.Combine(outputDir, "output.html");

            // Save the diagram as HTML using the custom stream provider
            diagram.Save(outputPath, htmlOptions);

            // Verify that the provider received identifiers for both images and shapes
            bool hasImage = false;
            bool hasShape = false;

            foreach (var id in provider.CapturedIdentifiers)
            {
                // Simple heuristic checks; adjust based on actual identifier format
                if (id.IndexOf("image", StringComparison.OrdinalIgnoreCase) >= 0)
                    hasImage = true;
                if (id.IndexOf("shape", StringComparison.OrdinalIgnoreCase) >= 0)
                    hasShape = true;
            }

            if (!hasImage || !hasShape)
            {
                throw new Exception("IStreamProvider did not receive expected resource identifiers for images and shapes.");
            }

            Console.WriteLine("HTML export completed successfully. Stream provider captured expected resource types.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}