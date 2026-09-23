using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ThumbnailStreamProvider : IStreamProvider
{
    private readonly Diagram _diagram;

    public ThumbnailStreamProvider(Diagram diagram)
    {
        _diagram = diagram;
    }

    // Called by Aspose.Diagram when it needs a resource stream (e.g., an image)
    public void InitStream(StreamProviderOptions options)
    {
        // The requested file name, e.g., "page0.png"
        string fileName = Path.GetFileName(options.DefaultPath);
        int pageIndex = 0;

        if (!string.IsNullOrEmpty(fileName) &&
            fileName.StartsWith("page", StringComparison.OrdinalIgnoreCase) &&
            fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
        {
            // Extract the numeric part between "page" and ".png"
            string numberPart = fileName.Substring(4, fileName.Length - 4 - 4);
            int.TryParse(numberPart, out pageIndex);
        }

        // Configure image export options for a PNG thumbnail
        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
        imgOptions.PageIndex = pageIndex;          // zero‑based page index
        imgOptions.Scale = 0.2f;                  // 20 % of original size (thumbnail)

        // Render the page to a memory stream
        MemoryStream ms = new MemoryStream();
        _diagram.Save(ms, imgOptions);
        ms.Position = 0;

        // Provide the stream to the HTML exporter
        options.Stream = ms;

        // Optional: set a custom virtual path for the resource
        options.CustomPath = $"thumbnails/{fileName}";
    }

    // Called after the HTML exporter finishes using the stream
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "sample.vsdx";

            // Output HTML file
            string outputHtml = "output.html";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new ThumbnailStreamProvider(diagram);

                // Export to HTML; thumbnails will be generated on‑the‑fly
                diagram.Save(outputHtml, htmlOptions);
            }

            Console.WriteLine("HTML export with embedded thumbnails completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}