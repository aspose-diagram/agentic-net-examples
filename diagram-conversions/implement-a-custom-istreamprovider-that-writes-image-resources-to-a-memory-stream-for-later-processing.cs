using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom IStreamProvider that stores each image resource in a MemoryStream.
    public class MemoryStreamProvider : IStreamProvider
    {
        // Stores streams keyed by the resource path (DefaultPath).
        private readonly Dictionary<string, MemoryStream> _streams = new Dictionary<string, MemoryStream>();

        // Called by Aspose.Diagram when a new resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Create a fresh memory stream for the resource.
            var memoryStream = new MemoryStream();

            // Assign the stream to the options so Aspose writes into it.
            options.Stream = memoryStream;

            // Keep a reference for later processing.
            _streams[options.DefaultPath] = memoryStream;
        }

        // Called when Aspose finishes writing to the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            // No special cleanup required; the stream remains in the dictionary.
            // If you need to reset the position for reading later, uncomment:
            // options.Stream.Position = 0;
        }

        // Retrieve the stored MemoryStream for a given resource path.
        public MemoryStream GetStream(string resourcePath)
        {
            return _streams.TryGetValue(resourcePath, out var stream) ? stream : null;
        }

        // Retrieve all stored streams.
        public IEnumerable<KeyValuePair<string, MemoryStream>> GetAllStreams()
        {
            return _streams;
        }
    }

    class Program
    {
        static void Main()
        {
            // Load or create a diagram (example uses an empty diagram).
            var diagram = new Diagram();

            // Configure HTML export options and assign the custom stream provider.
            var htmlOptions = new HTMLSaveOptions
            {
                StreamProvider = new MemoryStreamProvider()
            };

            // Export the diagram to HTML. Images referenced in the HTML will be written
            // to the MemoryStreamProvider instead of files on disk.
            string outputHtmlPath = "output.html";
            diagram.Save(outputHtmlPath, htmlOptions);

            // After saving, you can access the in‑memory image data.
            var provider = (MemoryStreamProvider)htmlOptions.StreamProvider;
            foreach (var kvp in provider.GetAllStreams())
            {
                string resourcePath = kvp.Key;          // e.g., "image1.png"
                MemoryStream imageStream = kvp.Value;   // Image data in memory

                // Example: write the image to a file for verification.
                string fileName = Path.GetFileName(resourcePath);
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    imageStream.Position = 0; // Ensure we read from the beginning.
                    imageStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Image resource '{resourcePath}' saved to file '{fileName}'.");
            }

            Console.WriteLine("HTML export completed.");
        }
    }
}