using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to compress exported resources using GZIP.
    public class GzipStreamProvider : IStreamProvider
    {
        // Keep track of the underlying memory streams for each options instance.
        private readonly Dictionary<StreamProviderOptions, MemoryStream> _memoryStreams = new();

        // Called before a resource stream is created.
        public void InitStream(StreamProviderOptions options)
        {
            // MemoryStream will hold the compressed data.
            var memory = new MemoryStream();

            // GZipStream writes compressed bytes into the memory stream.
            var gzip = new GZipStream(memory, CompressionMode.Compress, leaveOpen: true);

            // Assign the GZipStream to the options so Aspose writes into it.
            options.Stream = gzip;

            // Store the memory stream for later finalization.
            _memoryStreams[options] = memory;
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            if (!_memoryStreams.TryGetValue(options, out var memory))
                return;

            // Dispose the GZipStream to flush all data.
            options.Stream?.Dispose();

            // Reset position to read from the beginning.
            memory.Position = 0;

            // Write the compressed content to the target file path.
            using (var file = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write))
            {
                memory.CopyTo(file);
            }

            // Clean up.
            memory.Dispose();
            _memoryStreams.Remove(options);
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                var diagramPath = "input.vsdx";
                var diagram = new Diagram(diagramPath);

                // Configure HTML export options and assign the custom stream provider.
                var htmlOptions = new HTMLSaveOptions
                {
                    // Export all pages as separate files.
                    SaveAsSingleFile = false,
                    // Use the GZIP stream provider for resources (images, CSS, etc.).
                    StreamProvider = new GzipStreamProvider()
                };

                // Export the diagram to HTML. Resources will be written as compressed files.
                var outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine("HTML export completed with GZIP-compressed resources.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}