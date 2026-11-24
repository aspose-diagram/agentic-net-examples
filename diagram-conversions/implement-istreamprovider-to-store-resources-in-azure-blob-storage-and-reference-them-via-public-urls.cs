using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AzureBlobStreamProvider : IStreamProvider
{
    private readonly string _baseUrl;
    private readonly string _sasToken;
    private readonly HttpClient _httpClient;
    private readonly Dictionary<Stream, string> _streamPathMap = new();

    public AzureBlobStreamProvider(string baseUrl, string sasToken = "")
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _sasToken = sasToken;
        _httpClient = new HttpClient();
    }

    // Called by Aspose before writing a resource (image, shape, etc.)
    public void InitStream(StreamProviderOptions options)
    {
        // Create a memory stream that Aspose will write the resource into.
        MemoryStream ms = new MemoryStream();
        options.Stream = ms;

        // Store the relative path (default path) for later upload.
        // options.DefaultPath is read‑only, so we keep it in a dictionary keyed by the stream.
        _streamPathMap[ms] = options.DefaultPath;
    }

    // Called by Aspose after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Retrieve the memory stream that was used.
        if (options.Stream is not MemoryStream ms)
        {
            return;
        }

        // Get the relative path that Aspose intended for the resource.
        if (!_streamPathMap.TryGetValue(ms, out string relativePath))
        {
            return;
        }

        // Build the full URL for the blob (including SAS token if provided).
        string blobUrl = $"{_baseUrl}/{relativePath}";
        if (!string.IsNullOrEmpty(_sasToken))
        {
            blobUrl = $"{blobUrl}?{_sasToken}";
        }

        // Upload the content to Azure Blob Storage using HTTP PUT.
        // This uses only core .NET libraries; no external Azure SDK is required.
        try
        {
            ms.Position = 0; // Reset stream position before reading.
            var content = new StreamContent(ms);
            // Set content type based on file extension (basic handling).
            string ext = Path.GetExtension(relativePath).ToLowerInvariant();
            content.Headers.ContentType = ext switch
            {
                ".png" => new System.Net.Http.Headers.MediaTypeHeaderValue("image/png"),
                ".jpg" => new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"),
                ".jpeg" => new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"),
                ".gif" => new System.Net.Http.Headers.MediaTypeHeaderValue("image/gif"),
                ".svg" => new System.Net.Http.Headers.MediaTypeHeaderValue("image/svg+xml"),
                _ => new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream")
            };

            // Perform synchronous upload.
            HttpResponseMessage response = _httpClient.PutAsync(blobUrl, content).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to upload resource '{relativePath}' to Azure Blob Storage: {ex.Message}");
        }
        finally
        {
            // Clean up.
            ms.Dispose();
            _streamPathMap.Remove(ms);
        }

        // Update the default path to the public URL so that the generated HTML references the uploaded resource.
        // Since options.DefaultPath is read‑only, we cannot modify it directly.
        // However, Aspose uses the value we stored earlier; after upload, the HTML will reference the relative path.
        // If you need absolute URLs, you can post‑process the HTML file separately.
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expecting: [0] input Visio file, [1] output HTML file, [2] Azure Blob base URL, [3] optional SAS token
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath> <azureBlobBaseUrl> [sasToken]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string azureBaseUrl = args[2];
        string sasToken = args.Length >= 4 ? args[3] : string.Empty;

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new AzureBlobStreamProvider(azureBaseUrl, sasToken);

            // Export the diagram to HTML. The second argument must be a SaveOptions instance.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}