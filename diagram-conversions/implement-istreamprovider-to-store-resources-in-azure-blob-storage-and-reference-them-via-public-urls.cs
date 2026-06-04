using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to store resources locally (simulating Azure Blob Storage).
    public class AzureBlobStreamProvider : IStreamProvider
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _localRoot = "LocalBlobStorage";

        public AzureBlobStreamProvider(string connectionString, string containerName)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
        }

        // Called by Aspose when a resource stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Use DefaultPath as the relative file name.
            string relativePath = options.DefaultPath ?? Guid.NewGuid().ToString();
            string fullPath = Path.Combine(_localRoot, _containerName, relativePath);

            // Ensure the directory exists.
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Open a writable file stream.
            Stream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            options.Stream = fileStream;
        }

        // Called by Aspose after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Azure Blob Storage configuration (not used in this local simulation).
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net";
            string containerName = "diagram-resources";

            // Input Visio file and output HTML file.
            string inputPath = "input.vsdx";
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputPath = "output.html";

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new AzureBlobStreamProvider(connectionString, containerName)
                };

                // Export the diagram to HTML. Resources will be stored via the stream provider.
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML. Resources are stored using the custom stream provider.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during export: {ex.Message}");
            }
        }
    }
}