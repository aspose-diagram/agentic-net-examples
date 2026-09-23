using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Custom IStreamProvider that writes streams to a network location with retry logic
    public class NetworkStreamProvider : IStreamProvider
    {
        private readonly string _networkRoot;
        private readonly int _maxRetries;
        private readonly int _retryDelayMs;

        public NetworkStreamProvider(string networkRoot, int maxRetries = 3, int retryDelayMs = 1000)
        {
            _networkRoot = networkRoot;
            _maxRetries = maxRetries;
            _retryDelayMs = retryDelayMs;
        }

        // Called by Aspose.Diagram before writing a resource (e.g., images, CSS) to a stream
        public void InitStream(StreamProviderOptions options)
        {
            // Build the full network path for the resource
            string relativePath = options.DefaultPath; // e.g., "images/img1.png"
            string fullPath = Path.Combine(_networkRoot, relativePath);

            // Ensure the target directory exists
            string directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            int attempt = 0;
            while (true)
            {
                try
                {
                    // Create a writable file stream for the resource
                    options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    break; // Success
                }
                catch (IOException)
                {
                    attempt++;
                    if (attempt >= _maxRetries)
                    {
                        throw; // Re‑throw after exceeding retries
                    }
                    // Wait before retrying
                    Thread.Sleep(_retryDelayMs);
                }
            }
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            if (options.Stream != null)
            {
                options.Stream.Dispose();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram
                string inputDiagramPath = "sample.vsdx";

                // Network folder where HTML and its resources will be saved
                string networkRoot = @"\\networkshare\diagrams";

                // Load the diagram
                Diagram diagram = new Diagram(inputDiagramPath);

                // Configure HTML export options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new NetworkStreamProvider(networkRoot);

                // Output HTML file path (the main HTML file)
                string outputHtmlPath = Path.Combine(networkRoot, "output.html");

                // Export diagram to HTML using the custom provider
                diagram.Save(outputHtmlPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML with resources saved to the network location.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}