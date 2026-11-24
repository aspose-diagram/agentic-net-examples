using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Base directory on the networked file system where resources will be written.
    private readonly string _basePath;
    // Keep track of opened streams to close them later.
    private readonly Dictionary<string, Stream> _openStreams = new Dictionary<string, Stream>(StringComparer.OrdinalIgnoreCase);
    // Maximum number of retry attempts when opening a file stream.
    private const int MaxRetryAttempts = 3;
    // Delay between retries in milliseconds.
    private const int RetryDelayMs = 1000;

    public CustomStreamProvider(string basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
            throw new ArgumentException("Base path cannot be null or empty.", nameof(basePath));

        _basePath = basePath;
    }

    // Called by Aspose.Diagram before writing a resource (image, shape, etc.).
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // The relative path of the resource inside the HTML export (read‑only).
            string relativePath = options.DefaultPath ?? string.Empty;
            // Combine with the network base path to get the full file system path.
            string fullPath = Path.Combine(_basePath, relativePath);

            // Ensure the target directory exists.
            string directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Open the file stream with retry logic.
            Stream stream = OpenFileStreamWithRetry(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            // Assign the stream back to Aspose so it can write the resource.
            options.Stream = stream;

            // Store the stream for later disposal.
            _openStreams[fullPath] = stream;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"InitStream failed: {ex.Message}");
            throw;
        }
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            string relativePath = options.DefaultPath ?? string.Empty;
            string fullPath = Path.Combine(_basePath, relativePath);

            if (_openStreams.TryGetValue(fullPath, out Stream stream))
            {
                stream.Dispose();
                _openStreams.Remove(fullPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"CloseStream failed: {ex.Message}");
            // Swallowing exception to avoid breaking the overall save process.
        }
    }

    // Helper method that attempts to open a FileStream with retry logic.
    private Stream OpenFileStreamWithRetry(string path, FileMode mode, FileAccess access, FileShare share)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                return new FileStream(path, mode, access, share);
            }
            catch (IOException ioEx) when (attempt < MaxRetryAttempts)
            {
                attempt++;
                Console.Error.WriteLine($"IO exception opening '{path}'. Retry {attempt}/{MaxRetryAttempts} after delay. Details: {ioEx.Message}");
                Thread.Sleep(RetryDelayMs);
                // Continue to next attempt.
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - output HTML file path
        // 2 - network base directory for resources
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputHtmlPath = args.Length > 1 ? args[1] : "output.html";
        string networkBasePath = args.Length > 2 ? args[2] : @"\\networkshare\visio_resources";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the network base directory is reachable (optional but helpful).
        try
        {
            if (!Directory.Exists(networkBasePath))
            {
                Console.Error.WriteLine($"Network base directory does not exist: {networkBasePath}");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to access network base directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider(networkBasePath);

            // Save the diagram as HTML. Resources (images, shapes) will be written to the network location.
            diagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to '{outputHtmlPath}'.");
            Console.WriteLine($"Resources have been written to network directory '{networkBasePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}