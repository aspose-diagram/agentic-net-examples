using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AzureBlobStreamProvider : IStreamProvider
{
    private readonly string _baseUrl;
    private readonly string _sasToken;
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, MemoryStream> _streamMap;

    public AzureBlobStreamProvider(string baseUrl, string sasToken)
    {
        _baseUrl = baseUrl?.TrimEnd('/') ?? throw new ArgumentNullException(nameof(baseUrl));
        _sasToken = sasToken ?? throw new ArgumentNullException(nameof(sasToken));
        _httpClient = new HttpClient();
        _streamMap = new Dictionary<string, MemoryStream>();
    }

    // Called by Aspose before writing a resource (image, shape, etc.)
    public void InitStream(StreamProviderOptions options)
    {
        // Create a memory stream to capture the resource data.
        var memStream = new MemoryStream();
        // Store the stream so it can be uploaded later in CloseStream.
        _streamMap[options.DefaultPath] = memStream;
        // Assign the stream to the options so Aspose writes into it.
        options.Stream = memStream;
    }

    // Called by Aspose after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (!_streamMap.TryGetValue(options.DefaultPath, out var memStream))
        {
            // No stream was captured; nothing to upload.
            return;
        }

        try
        {
            // Reset position to the beginning before uploading.
            memStream.Position = 0;

            // Construct the full Azure Blob URL with SAS token.
            string uploadUrl = $"{_baseUrl}/{options.DefaultPath}?{_sasToken}";

            // Prepare HTTP content.
            using var content = new StreamContent(memStream);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            // Perform synchronous upload.
            var response = _httpClient.PutAsync(uploadUrl, content).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($"Failed to upload '{options.DefaultPath}' to Azure Blob Storage. Status: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error uploading '{options.DefaultPath}': {ex.Message}");
        }
        finally
        {
            // Clean up.
            memStream.Dispose();
            _streamMap.Remove(options.DefaultPath);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - Input Visio file path
        // 1 - Output HTML file path
        // 2 - Azure Blob Storage base URL (e.g., https://myaccount.blob.core.windows.net/container)
        // 3 - SAS token (without leading '?')
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath> <azureBaseUrl> <sasToken>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string azureBaseUrl = args[2];
        string sasToken = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options with custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new AzureBlobStreamProvider(azureBaseUrl, sasToken);

            // Save the diagram as HTML. The StreamProvider will handle resource uploads.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}