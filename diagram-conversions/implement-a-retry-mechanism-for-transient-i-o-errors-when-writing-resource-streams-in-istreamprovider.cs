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
        private const int MaxRetryAttempts = 3;
        private const int RetryDelayMilliseconds = 200;

        // Helper method to execute an action with retry on IOException.
        private static void ExecuteWithRetry(Action action)
        {
            int attempt = 0;
            while (true)
            {
                try
                {
                    action();
                    break; // Success
                }
                catch (IOException ex) when (attempt < MaxRetryAttempts)
                {
                    attempt++;
                    // Simple back‑off before retrying.
                    Thread.Sleep(RetryDelayMilliseconds);
                }
                catch
                {
                    // Non‑IO or max attempts exceeded – rethrow.
                    throw;
                }
            }
        }

        // Called by Aspose.Diagram when a resource stream needs to be created.
        public void InitStream(StreamProviderOptions options)
        {
            // options.DefaultPath provides the target file path for the resource.
            string targetPath = options.DefaultPath;

            ExecuteWithRetry(() =>
            {
                // Ensure the directory exists.
                string directory = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create the file stream for writing.
                FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
                // Assign the stream back to the options object.
                options.Stream = fileStream;
            });
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            ExecuteWithRetry(() =>
            {
                // Close and dispose the stream if it was created.
                options.Stream?.Dispose();
                options.Stream = null;
            });
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options and assign the retry stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export all pages.
                    PageCount = int.MaxValue,
                    // Use the custom stream provider for resource files.
                    StreamProvider = new RetryStreamProvider()
                };

                // Export the diagram to HTML. Resources (images, CSS, etc.) will be written via the provider.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine("HTML export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}