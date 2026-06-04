using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportWithRetry
{
    // Implements IStreamProvider with retry logic for transient I/O errors.
    public class RetryStreamProvider : IStreamProvider
    {
        private const int MaxRetries = 3;
        private const int DelayMilliseconds = 2000;

        // Called by Aspose when a resource stream needs to be created.
        public void InitStream(StreamProviderOptions options)
        {
            // The DefaultPath property contains the relative path for the resource (e.g., images/style.css).
            string filePath = options.DefaultPath;

            // Ensure the directory exists.
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            int attempt = 0;
            while (true)
            {
                try
                {
                    // Create the file stream for writing the resource.
                    options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    break; // Success
                }
                catch (IOException)
                {
                    attempt++;
                    if (attempt >= MaxRetries)
                    {
                        throw; // Re‑throw after exceeding retry count.
                    }
                    // Wait before retrying.
                    Thread.Sleep(DelayMilliseconds);
                }
            }
        }

        // Called by Aspose after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Close and dispose the stream if it was created.
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing diagram (replace with your actual file path).
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new RetryStreamProvider(),
                    // Optional: set other options as needed.
                    SaveAsSingleFile = false,
                    PageIndex = 0,
                    PageCount = int.MaxValue
                };

                // Define the output HTML file path.
                string outputHtml = "output.html";

                // Save the diagram to HTML using the retry‑enabled stream provider.
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine("Diagram exported to HTML successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}