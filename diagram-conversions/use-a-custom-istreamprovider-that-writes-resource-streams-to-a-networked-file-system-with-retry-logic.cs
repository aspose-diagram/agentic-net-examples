using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Custom IStreamProvider that writes resource streams to a networked file system with retry logic
    public class NetworkStreamProvider : IStreamProvider
    {
        // Maximum number of retry attempts
        private const int MaxRetryAttempts = 3;
        // Delay between retries in milliseconds
        private const int RetryDelayMs = 1000;

        // Called by Aspose.Diagram when a resource stream needs to be created
        public void InitStream(StreamProviderOptions options)
        {
            // The DefaultPath property contains the base path for the resource (e.g., image name)
            // Build the full network path where the resource will be saved
            string networkBasePath = @"\\NetworkShare\DiagramResources\"; // adjust to your network share
            string resourceFileName = Path.GetFileName(options.DefaultPath);
            string fullPath = Path.Combine(networkBasePath, resourceFileName);

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Retry logic for creating the file stream
            for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
            {
                try
                {
                    // Create a writable file stream for the resource
                    FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    // Assign the stream back to the options so Aspose can write to it
                    options.Stream = fileStream;
                    break; // success, exit retry loop
                }
                catch (IOException)
                {
                    if (attempt == MaxRetryAttempts)
                    {
                        // Re‑throw after final attempt
                        throw;
                    }
                    // Wait before next retry
                    Thread.Sleep(RetryDelayMs);
                }
            }
        }

        // Called by Aspose.Diagram after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Close the stream if it was created
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
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new NetworkStreamProvider();
                htmlOptions.SaveAsSingleFile = false; // generate separate resource files
                htmlOptions.PageCount = int.MaxValue; // export all pages

                // Export the diagram to HTML; resources will be written via NetworkStreamProvider
                string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML with resources saved to network location.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}