using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class FileStreamProvider : IStreamProvider
{
    private readonly string _resourceBasePath;

    public FileStreamProvider(string resourceBasePath)
    {
        _resourceBasePath = resourceBasePath;
    }

    // Called by Aspose when a resource stream is required during HTML export.
    public void InitStream(StreamProviderOptions options)
    {
        // The requested resource name is provided via DefaultPath (read‑only).
        string requestedName = options.DefaultPath ?? string.Empty;
        string fullPath = Path.Combine(_resourceBasePath, requestedName);

        if (File.Exists(fullPath))
        {
            // Resource exists – open a read‑only stream.
            options.Stream = File.OpenRead(fullPath);
        }
        else
        {
            // Resource missing – log the error and supply an empty stream to avoid null reference.
            Console.Error.WriteLine($"[Error] Missing resource for HTML export: '{requestedName}'. Expected at '{fullPath}'.");
            // Provide an empty stream so the export can continue without throwing.
            options.Stream = new MemoryStream();
        }
    }

    // Called by Aspose after the stream is no longer needed.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Dispose();
            options.Stream = null;
        }
    }
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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output HTML file path.
        string outputPath = "output.html";

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Set up the stream provider to handle external resources (images, shapes, etc.).
            // The base folder for resources is the directory containing the input file.
            string resourceBaseFolder = Path.GetDirectoryName(Path.GetFullPath(inputPath));
            htmlOptions.StreamProvider = new FileStreamProvider(resourceBaseFolder);

            // Perform the HTML export.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}