using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MissingResourceStreamProvider : IStreamProvider
{
    private readonly string _resourceFolder;

    // Constructor receives the folder where resources (images, shapes) are expected.
    public MissingResourceStreamProvider(string resourceFolder)
    {
        _resourceFolder = resourceFolder;
    }

    // Called by Aspose.Diagram when it needs a stream for a specific resource.
    public void InitStream(StreamProviderOptions options)
    {
        // The requested resource name is provided via DefaultPath (read‑only).
        string resourcePath = Path.Combine(_resourceFolder, options.DefaultPath);

        if (File.Exists(resourcePath))
        {
            // Resource exists – open a read‑only file stream.
            options.Stream = new FileStream(resourcePath, FileMode.Open, FileAccess.Read);
        }
        else
        {
            // Resource missing – log the error and supply an empty stream to avoid null references.
            Console.Error.WriteLine($"Missing resource: {resourcePath}");
            options.Stream = new MemoryStream(); // Empty placeholder stream.
        }
    }

    // Called after Aspose.Diagram finishes using the stream.
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
        // Expected arguments:
        // 0 - input Visio diagram path
        // 1 - output HTML file path
        // 2 - (optional) folder containing external resources (images, shapes)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputHtmlPath> [resourceFolder]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string resourceFolder = args.Length >= 3 ? args[2] : Path.GetDirectoryName(inputPath);

        // Guard: ensure the input diagram file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the resource folder exists (if provided).
        if (!Directory.Exists(resourceFolder))
        {
            Console.Error.WriteLine($"Resource folder not found: {resourceFolder}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new MissingResourceStreamProvider(resourceFolder);

            // Save the diagram as HTML.
            diagram.Save(outputPath, htmlOptions);
            Console.WriteLine($"Diagram successfully saved to HTML at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any errors during loading or saving.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}