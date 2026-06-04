using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Custom IStreamProvider that writes resource streams to a network file system with retry logic
    public class NetworkFileStreamProvider : IStreamProvider
    {
        // Base folder on the network where resources will be stored
        private readonly string _networkBasePath = @"\\networkshare\diagram_resources";

        // Number of retry attempts for creating the stream
        private const int MaxRetryAttempts = 3;

        // Delay between retries in milliseconds
        private const int RetryDelayMs = 1000;

        // Called by Aspose.Diagram before writing a resource stream
        public void InitStream(StreamProviderOptions options)
        {
            // Ensure the default path is available (e.g., "images/img1.png")
            string relativePath = options.DefaultPath ?? string.Empty;

            // Combine base network path with the relative resource path
            string fullPath = Path.Combine(_networkBasePath, relativePath);

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Retry logic for creating the file stream
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
                    if (attempt >= MaxRetryAttempts)
                    {
                        // Re‑throw after max attempts
                        throw;
                    }
                    // Wait before retrying
                    Thread.Sleep(RetryDelayMs);
                }
            }
        }

        // Called by Aspose.Diagram after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Safely close the stream if it was created
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (adjust the path as needed)
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export each page as separate files (default behavior)
                    SaveAsSingleFile = false,
                    // Assign our network‑aware stream provider
                    StreamProvider = new NetworkFileStreamProvider()
                };

                // Export the diagram to HTML; resources (images, CSS, etc.) will be written via the provider
                string outputHtmlPath = @"C:\Exported\diagram.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                // Simple confirmation
                Console.WriteLine("Diagram exported to HTML with resources saved to the network location.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}