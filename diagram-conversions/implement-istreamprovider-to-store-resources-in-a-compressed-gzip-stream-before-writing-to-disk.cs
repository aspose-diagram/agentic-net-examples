using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to compress each resource using GZIP before writing to disk.
    public class GzipStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram when a new resource stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            // Ensure the target directory exists.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a file stream for the resource and wrap it with GZipStream for compression.
            FileStream fileStream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
            options.Stream = new GZipStream(fileStream, CompressionMode.Compress);
        }

        // Called after the resource has been written; dispose the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options and assign the custom GZIP stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new GzipStreamProvider()
                };

                // Export the diagram to HTML; resources (images, CSS, etc.) will be compressed.
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("Diagram exported to HTML with GZIP-compressed resources.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}