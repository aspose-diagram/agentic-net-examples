using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    private readonly string _baseUrl;

    public CustomStreamProvider(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    // Called by Aspose.Diagram during HTML export to initialize the stream provider.
    public void InitStream(StreamProviderOptions options)
    {
        // Set the custom base URL that will be prefixed to all linked resources (images, CSS, etc.).
        options.CustomPath = _baseUrl;
        // No actual stream is assigned because we only need to control the URL path.
    }

    // Called after the export is finished; no resources to release in this simple implementation.
    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup required.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Output HTML file path (second argument or default).
        string outputPath = args.Length > 1 ? args[1] : "output.html";

        // Custom base URL for linked resources (third argument or default).
        string baseUrl = args.Length > 2 ? args[2] : "http://example.com/resources/";

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

            // Configure HTML save options with a custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Assign the custom stream provider to control resource URLs.
                StreamProvider = new CustomStreamProvider(baseUrl)
            };

            // Save the diagram as HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML at '{outputPath}' with base URL '{baseUrl}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}