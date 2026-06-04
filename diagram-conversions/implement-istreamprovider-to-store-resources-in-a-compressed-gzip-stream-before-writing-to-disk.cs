using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace GzipHtmlExport
{
    // Implements IStreamProvider to compress exported resources using GZIP.
    public class GzipStreamProvider : IStreamProvider
    {
        // Called before a resource stream is created.
        public void InitStream(StreamProviderOptions options)
        {
            // The path where the resource should be saved.
            string targetPath = options.DefaultPath;

            // Create a file stream for the target path.
            FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);

            // Wrap the file stream with GZip compression.
            GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);

            // Assign the compressed stream back to the options.
            options.Stream = gzipStream;
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream (which also disposes the underlying file stream).
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed).
                string inputPath = "input.vsdx";

                // Output HTML file path.
                string outputPath = "output.html";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new GzipStreamProvider()
                };

                // Save the diagram as HTML; resources will be compressed via GZIP.
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML with GZIP-compressed resources.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}