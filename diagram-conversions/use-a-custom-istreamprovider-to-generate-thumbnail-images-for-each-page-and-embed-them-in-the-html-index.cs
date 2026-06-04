using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramThumbnailHtmlExport
{
    // Custom stream provider that generates a thumbnail image for each page on demand.
    class ThumbnailStreamProvider : IStreamProvider
    {
        private readonly Diagram _diagram;

        public ThumbnailStreamProvider(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
        }

        // Called by Aspose when a resource (e.g., an image) is requested during HTML export.
        public void InitStream(StreamProviderOptions options)
        {
            // The DefaultPath contains the requested file name, e.g., "page1.png".
            // Extract the page index (0‑based) from the file name.
            int pageIndex = ExtractPageIndex(options.DefaultPath);

            // Prepare image save options for a PNG thumbnail.
            var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                PageIndex = pageIndex,
                // Reduce size for thumbnail; adjust as needed.
                Scale = 0.2f
            };

            // Render the page to a memory stream.
            var ms = new MemoryStream();
            _diagram.Save(ms, imgOptions);
            ms.Position = 0; // Reset for reading.

            // Assign the stream back to the provider options.
            options.Stream = ms;
        }

        // Called after the resource has been written; clean up the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Dispose();
        }

        // Helper to parse the page index from a file name like "page0.png".
        private static int ExtractPageIndex(string defaultPath)
        {
            if (string.IsNullOrEmpty(defaultPath))
                return 0;

            var match = Regex.Match(defaultPath, @"page(\d+)", RegexOptions.IgnoreCase);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int index))
                return index;

            // Fallback to first page if parsing fails.
            return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect the input Visio file path as the first argument.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramThumbnailHtmlExport <input.vsdx>");
                return;
            }

            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the diagram.
            using var diagram = new Diagram(inputPath);

            // Configure HTML export with the custom stream provider.
            var htmlOptions = new HTMLSaveOptions
            {
                // Use the provider to embed per‑page thumbnails.
                StreamProvider = new ThumbnailStreamProvider(diagram),

                // Optional: do not export hidden pages.
                ExportHiddenPage = false,

                // Optional: set a title for the HTML document.
                Title = Path.GetFileNameWithoutExtension(inputPath)
            };

            // Save the HTML index (images will be generated on the fly).
            string outputHtml = Path.ChangeExtension(inputPath, "html");
            diagram.Save(outputHtml, htmlOptions);

            Console.WriteLine($"HTML export completed: {outputHtml}");
        }
    }
}