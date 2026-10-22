using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output HTML file path (required)
        string outputHtmlPath = args.Length > 1 ? args[1] : "output.html";

        // UNC folder where HTML resources (images, etc.) will be written (required)
        string uncResourceFolder = args.Length > 2 ? args[2] : @"\\server\share\visio_resources";

        // Ensure the UNC directory exists
        try
        {
            Directory.CreateDirectory(uncResourceFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create UNC directory '{uncResourceFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with a custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new NetworkShareStreamProvider(uncResourceFolder);

            // Save the diagram as HTML; resources will be written to the UNC share via the stream provider
            diagram.Save(outputHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

/// <summary>
/// Implements IStreamProvider to write HTML resources (images, CSS, etc.) to a network share (UNC path).
/// </summary>
class NetworkShareStreamProvider : IStreamProvider
{
    private readonly string _baseFolder;

    public NetworkShareStreamProvider(string baseFolder)
    {
        _baseFolder = baseFolder;
    }

    // Called by Aspose.Diagram when a resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base UNC folder with the default relative path provided by Aspose.
        string resourcePath = Path.Combine(_baseFolder, options.DefaultPath);

        // Ensure the directory for the resource exists.
        string directory = Path.GetDirectoryName(resourcePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream for writing the resource.
        options.Stream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);

        // Set the custom base URL/path that will be used inside the generated HTML.
        // This tells the HTML where to locate the resources.
        options.CustomPath = _baseFolder;
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Safely close the stream if it was created.
        if (options.Stream != null)
        {
            options.Stream.Dispose();
            options.Stream = null;
        }
    }
}