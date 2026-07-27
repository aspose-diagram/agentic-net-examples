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
        private const int MaxRetries = 3;          // Maximum number of retry attempts.
        private const int DelayMilliseconds = 500; // Delay between retries.

        // Called by Aspose.Diagram when a resource stream needs to be created.
        public void InitStream(StreamProviderOptions options)
        {
            // The path where the resource (e.g., image) should be written.
            string path = options.DefaultPath;

            // Ensure the target directory exists.
            string directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            int attempt = 0;
            while (true)
            {
                try
                {
                    // Open the file stream for writing.
                    options.Stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                    break; // Success – exit the retry loop.
                }
                catch (IOException ex) when (IsTransient(ex) && attempt < MaxRetries)
                {
                    // Transient error – wait and retry.
                    attempt++;
                    Thread.Sleep(DelayMilliseconds);
                }
                catch
                {
                    // Non‑transient error or max retries exceeded – rethrow.
                    throw;
                }
            }
        }

        // Called by Aspose.Diagram after the resource stream is no longer needed.
        public void CloseStream(StreamProviderOptions options)
        {
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }

        // Simple heuristic to decide if an IOException is transient.
        private bool IsTransient(IOException ex)
        {
            // For demonstration, treat all IOExceptions as transient.
            // In production, inspect HResult or inner exceptions for more precise detection.
            return true;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file.
                string inputPath = "input.vsdx";

                // Output HTML file.
                string outputPath = "output.html";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML save options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new RetryStreamProvider()
                };

                // Save the diagram as HTML using the options.
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}