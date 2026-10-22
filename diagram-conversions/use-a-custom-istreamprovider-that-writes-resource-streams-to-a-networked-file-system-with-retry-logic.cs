using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class NetworkStreamProvider : IStreamProvider
{
    private readonly string _networkBasePath;
    private const int MaxRetries = 3;
    private const int DelayMilliseconds = 1000;

    public NetworkStreamProvider(string networkBasePath)
    {
        _networkBasePath = networkBasePath;
    }

    // Initialize the stream for a given resource using retry logic
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the network base path with the default relative path provided by Aspose
        string relativePath = options.DefaultPath ?? string.Empty;
        string fullPath = Path.Combine(_networkBasePath, relativePath);

        // Ensure the directory exists
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Attempt to create the file stream with retries
        int attempt = 0;
        while (true)
        {
            try
            {
                // Assign a writable FileStream to the options
                options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                break; // Success
            }
            catch (IOException ex)
            {
                attempt++;
                if (attempt >= MaxRetries)
                {
                    Console.Error.WriteLine($"Failed to create stream for '{fullPath}' after {MaxRetries} attempts: {ex.Message}");
                    throw;
                }
                // Wait before retrying
                Thread.Sleep(DelayMilliseconds);
            }
        }
    }

    // Close the stream after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            try
            {
                options.Stream.Close();
                options.Stream.Dispose();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error closing stream: {ex.Message}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output HTML file path, network base folder for resources
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputHtmlPath> <networkBaseFolder>");
            return;
        }

        string inputDiagramPath = args[0];
        string outputHtmlPath = args[1];
        string networkBaseFolder = args[2];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: ensure network base folder exists (create if necessary)
        if (!Directory.Exists(networkBaseFolder))
        {
            try
            {
                Directory.CreateDirectory(networkBaseFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create network base folder '{networkBaseFolder}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Configure HTML save options with custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new NetworkStreamProvider(networkBaseFolder);

            // Save the diagram as HTML; resources (images, shapes) will be written via the stream provider
            diagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML at '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}