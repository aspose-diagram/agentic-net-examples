using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class LoggingStreamProvider : IStreamProvider
{
    private string _basePath;

    // Called before any streams are requested; store the default path.
    public void InitStream(StreamProviderOptions options)
    {
        _basePath = options.DefaultPath;
        Console.WriteLine($"[StreamProvider] Initialized. Base path: {_basePath}");
    }

    // Called for each resource (image, shape, etc.) that needs a stream.
    public Stream GetStream(string resourcePath)
    {
        // Log the creation of the resource stream.
        Console.WriteLine($"[StreamProvider] Creating stream for resource: {resourcePath}");

        // Combine the base path with the relative resource path.
        string fullPath = Path.Combine(_basePath ?? string.Empty, resourcePath);

        // Ensure the directory exists.
        string directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Return a writable file stream for the resource.
        return new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Called after all streams have been processed; perform any cleanup if needed.
    public void CloseStream(StreamProviderOptions options)
    {
        Console.WriteLine("[StreamProvider] Closed.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram.
        string inputPath = "sample.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the file.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new LoggingStreamProvider();

            // Output HTML file path.
            string outputPath = "output.html";

            // Save the diagram as HTML; resources will be handled by the stream provider.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}