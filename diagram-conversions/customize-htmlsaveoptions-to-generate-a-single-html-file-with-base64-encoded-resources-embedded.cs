using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Base64StreamProvider : IStreamProvider
{
    // Stores the final byte content of each resource keyed by its default path.
    private readonly Dictionary<string, byte[]> _resourceData = new Dictionary<string, byte[]>();
    // Holds the active MemoryStream while the resource is being written.
    private readonly Dictionary<string, MemoryStream> _activeStreams = new Dictionary<string, MemoryStream>();

    public void InitStream(StreamProviderOptions options)
    {
        // Create a memory stream for the resource and assign it to the options.
        var ms = new MemoryStream();
        options.Stream = ms;
        _activeStreams[options.DefaultPath] = ms;
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // When the resource writing is finished, capture the bytes.
        if (_activeStreams.TryGetValue(options.DefaultPath, out var ms))
        {
            _resourceData[options.DefaultPath] = ms.ToArray();
            ms.Dispose();
            _activeStreams.Remove(options.DefaultPath);
        }
    }

    // Expose the collected resources for post‑processing.
    public IReadOnlyDictionary<string, byte[]> ResourceData => _resourceData;
}

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and optional output HTML path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputHtmlPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.ChangeExtension(inputPath, ".html");

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden pages/shapes to keep the HTML clean.
                diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                // Set up HTML save options with a custom stream provider.
                var streamProvider = new Base64StreamProvider();
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions()
                {
                    ExportHiddenPage = false,
                    StreamProvider = streamProvider
                };

                // Save the diagram to HTML (resources will be captured by the provider).
                diagram.Save(outputPath, htmlOptions);

                // After saving, embed captured resources as base64 data URIs.
                EmbedResourcesAsBase64(outputPath, streamProvider);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }

    // Replaces external resource references in the HTML with base64 data URIs.
    private static void EmbedResourcesAsBase64(string htmlFilePath, Base64StreamProvider provider)
    {
        if (!File.Exists(htmlFilePath))
        {
            Console.Error.WriteLine($"Generated HTML file not found: {htmlFilePath}");
            return;
        }

        string htmlContent = File.ReadAllText(htmlFilePath, Encoding.UTF8);

        foreach (var kvp in provider.ResourceData)
        {
            string resourcePath = kvp.Key; // This is the default path used by Aspose.
            string fileName = Path.GetFileName(resourcePath);
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            // Determine MIME type based on file extension.
            string mime = GetMimeType(extension);
            if (mime == null)
                continue; // Skip unknown types.

            string base64 = Convert.ToBase64String(kvp.Value);
            string dataUri = $"data:{mime};base64,{base64}";

            // Replace all occurrences of the resource file name in the HTML.
            // The HTML may reference the resource with or without path; handle both.
            htmlContent = htmlContent.Replace($"src=\"{fileName}\"", $"src=\"{dataUri}\"");
            htmlContent = htmlContent.Replace($"href=\"{fileName}\"", $"href=\"{dataUri}\"");
        }

        // Write the modified HTML back to the same file.
        File.WriteAllText(htmlFilePath, htmlContent, Encoding.UTF8);
    }

    // Simple MIME type resolver for common image formats.
    private static string GetMimeType(string extension)
    {
        return extension switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".svg" => "image/svg+xml",
            ".bmp" => "image/bmp",
            _ => null,
        };
    }
}