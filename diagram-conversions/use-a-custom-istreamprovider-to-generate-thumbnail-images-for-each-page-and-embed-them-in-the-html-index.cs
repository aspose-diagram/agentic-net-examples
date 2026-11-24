using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <OutputDirectory>");
            return;
        }

        string visioPath = args[0];
        string outputDir = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Ensure the output directory exists.
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Configure HTML save options with a custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new ThumbnailStreamProvider(diagram);

            // Save the HTML index file.
            string htmlPath = Path.Combine(outputDir, "index.html");
            diagram.Save(htmlPath, htmlOptions);

            Console.WriteLine($"HTML export completed: {htmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

/// <summary>
/// Custom IStreamProvider that generates thumbnail PNG images for each page on demand
/// and supplies them to the HTML export process.
/// </summary>
class ThumbnailStreamProvider : IStreamProvider
{
    private readonly Diagram _diagram;
    private readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
    private static readonly Regex _pageImageRegex = new Regex(@"page(\d+)_\d+\.png$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public ThumbnailStreamProvider(Diagram diagram)
    {
        _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
    }

    // Called by Aspose when a resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        string resourcePath = options.DefaultPath ?? string.Empty;

        // If we have already generated this resource, reuse it.
        if (_cache.TryGetValue(resourcePath, out byte[] cachedBytes))
        {
            options.Stream = new MemoryStream(cachedBytes);
            return;
        }

        // Determine if the requested resource is a page thumbnail image.
        Match match = _pageImageRegex.Match(resourcePath);
        if (match.Success && int.TryParse(match.Groups[1].Value, out int pageIndex))
        {
            // Generate thumbnail for the requested page.
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.PageIndex = pageIndex;
            imgOptions.PageCount = 1;
            // Optional: set a reasonable resolution for thumbnails.
            imgOptions.Resolution = 96f;

            using (MemoryStream ms = new MemoryStream())
            {
                // Save the specific page as PNG into the memory stream.
                _diagram.Save(ms, imgOptions);
                byte[] imageBytes = ms.ToArray();

                // Cache the generated image for potential reuse.
                _cache[resourcePath] = imageBytes;

                // Provide the stream to Aspose.
                options.Stream = new MemoryStream(imageBytes);
            }
        }
        else
        {
            // For any other resource, provide an empty stream to avoid errors.
            options.Stream = new MemoryStream();
        }
    }

    // Called by Aspose after the stream is no longer needed.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options?.Stream != null)
        {
            try
            {
                options.Stream.Dispose();
            }
            catch
            {
                // Suppress any disposal exceptions.
            }
        }
    }
}