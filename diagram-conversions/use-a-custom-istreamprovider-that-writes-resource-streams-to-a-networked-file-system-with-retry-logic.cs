using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Custom stream provider that writes resources to a network share with retry logic
    public class NetworkStreamProvider : IStreamProvider
    {
        // Base network folder where resources will be saved
        private const string NetworkBasePath = @"\\networkshare\diagrams";

        // Number of retry attempts
        private const int MaxRetries = 3;

        // Delay between retries in milliseconds
        private const int RetryDelayMs = 1000;

        public void InitStream(StreamProviderOptions options)
        {
            // The relative path of the resource to be saved (e.g., image file name)
            string relativePath = options.DefaultPath;

            // Combine with the network base path
            string fullPath = Path.Combine(NetworkBasePath, relativePath);

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Attempt to create the file stream with retry logic
            for (int attempt = 1; attempt <= MaxRetries; attempt++)
            {
                try
                {
                    // Create a writable file stream for the resource
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    // Assign the stream to the options object
                    options.Stream = fileStream;
                    break; // Success, exit the retry loop
                }
                catch (IOException)
                {
                    if (attempt == MaxRetries)
                    {
                        // All attempts failed, rethrow the exception
                        throw;
                    }
                    // Wait before the next retry
                    Thread.Sleep(RetryDelayMs);
                }
            }
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created
            if (options.Stream != null)
            {
                options.Stream.Dispose();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("sample.vsdx");

                // Configure HTML export options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new NetworkStreamProvider()
                };

                // Export the diagram to HTML; resources (images, CSS, etc.) will be written via the provider
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}